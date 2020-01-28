from bs4 import BeautifulSoup
import requests
import numpy as np
import pymongo
import os
import sqlite3 as sq

class Course:

    def __init__(self, code, points, pointType, name, description, representative, study_periods, table_index):
        self.code = code
        self.points = points
        self.pointType = pointType
        self.name = name
        self.description = description
        self.representative = representative
        self.study_periods = study_periods
        self.table_index = table_index
    
    
    #prints the course data to courses.txt in a ';' seperated list format
    def printToFile(self, specializations):
        file = open('courses.txt', 'a')
        
        temp = ''
        
        for period in self.study_periods:
            temp += str(period) + '/'
        
        courseString = self.code.rstrip() + ';' + self.points + ';' + self.pointType + ';' + self.name + ';' + self.description + ';' + self.representative + ';' + temp + ';' + specializations[self.table_index] + ';'
        file.write(courseString)
        file.close()


#fetches the course data from lth database
def fetch_courses(program):
    print('Fetching courses for %s from website' % program)
    resp = requests.get('https://kurser.lth.se/lot/?val=program&prog=%s' % program).content
    return resp

    
def scrape():
    programmes = ['d']
    #programmes = ['a', 'b', 'c', 'd', 'e', 'f', 'i', 'k', 'l', 'm', 'v', 'w']

    for program in programmes:
        all_courses = fetch_courses(program)
        soup = BeautifulSoup(all_courses, 'lxml')
        
    
    headings = soup.find_all('h3')
    
    
    specializations = []
    
    for h in headings:
        specializations.append(h.text)
    
    tables = soup.find_all('table')

    #removes file if it exists
    if(os.path.exists("courses.txt")): 
        os.remove('courses.txt')
        
    #creates course objects from the scraped html content
    for i, t in enumerate(tables):
        tab = t.find_all('tr')
        for table in tab:
            child_nodes = table.find_all('td', recursive=False)[0:3]    
            td_length = len(table.find_all('td'))
            if(len(child_nodes) == 3):
                code = child_nodes[0].text
                points = child_nodes[1].text
                pointType = child_nodes[2].text
                name = ''
                description = ''
                if (td_length == 13):
                    name = table.find_all('td')[5].text
                elif(td_length == 16):
                    name = table.find_all('td')[8].text
                elif(td_length == 15):
                    name = table.find_all('td')[7].text

                study_period_entries = table.find_all('td', class_='bg_blue')
                study_periods = []
                if (len(study_period_entries) > 0):
                    for entry in study_period_entries:
                        study_periods.append(entry.find('strong').text)
                
                
                course_description_webpage = table.find_all('td', class_ = 'pdf-avoidlinebreak')
                course_description_webpage.append(table.find_all('td', class_ = 'pdf-avoidlinebreak.bg_pink'))
                resp = requests.get(course_description_webpage[0].find('a')['href']).content
                temp_soup = BeautifulSoup(resp, 'lxml')
                description = temp_soup.find_all('p')[2].text
                description.strip('\n')
                
                representative = ''
                representative_list = temp_soup.find_all("a", href=lambda href: href and "@" in href)
                
                if (len(representative_list) == 0):
                    representative = ''
                else:
                    representative = representative_list[0].text
                
                if(code != '' and points != '' and pointType != '' and name != '' and description != '' and study_periods != []):
                    course = Course(code, points, pointType, name, description.replace(';', ' '), representative, study_periods, i)
                    course.printToFile(specializations)

if __name__ == '__main__':
    scrape()
    create_database()