import pyodbc 
import pandas
import sqlite3
import pyodbc 
conn = pyodbc.connect('Driver={SQL Server};'
                      'Server=LAPTOP-07HAFPS9\SQLEXPRESS;'
                      'Database=Courses;'
                      'Trusted_Connection=yes;')

cursor = conn.cursor()

file = open('courses.txt', 'r')
temp = file.read()

temp.strip('\n')
dataframe = temp.split(';')
length = (int)(len(dataframe)/8)

for i in range(0 , length):
    if 'obligatoriska' in dataframe[7 + i*8]:
        sql = "INSERT INTO base_course_info(code, points, point_type, name, description, representative, study_periods, specialization) VALUES (?,?,?,?,?,?,?,?)"
    else:
        sql = "INSERT INTO spec_course_info(code, points, point_type, name, description, representative, study_periods, specialization) VALUES (?,?,?,?,?,?,?,?)"
    cursor.execute(sql, (dataframe[0 + i*8], dataframe[1 + i*8].replace(',' , '.'), dataframe[2 + i*8], dataframe[3 + i*8], dataframe[4 + i*8], dataframe[5 + i*8], dataframe[6 + i*8], dataframe[7 + i*8]))
conn.commit()

#cursor.execute("SELECT SUM(points) FROM course_info")

