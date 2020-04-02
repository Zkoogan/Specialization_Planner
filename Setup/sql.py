import pyodbc 
import sqlite3

conn = pyodbc.connect("Driver={SQLite3 ODBC Driver};Database=Courses.db")

cursor = conn.cursor()

file = open('courses.txt', 'r')
temp = file.read()

temp.strip('\n')
dataframe = temp.split(';')
length = (int)(len(dataframe)/8)

for i in range(0 , length):
    if 'obligatoriska' in dataframe[7 + i*8]:
        sql = "INSERT INTO base_course_info(Kurskod, Antal_poäng, Poängtyp, Kursnamn, Beskrivning, Representant_email, Läsperiod, Specialisering) VALUES (?,?,?,?,?,?,?,?)"
        cursor.execute(sql, (dataframe[0 + i*8], dataframe[1 + i*8].replace(',' , '.'), dataframe[2 + i*8], dataframe[3 + i*8], dataframe[4 + i*8], dataframe[5 + i*8],dataframe[6 + i*8],dataframe[7 + i*8]))
    else:
        sql = "INSERT OR IGNORE INTO spec_course_info(Kurskod, Antal_poäng, Poängtyp, Kursnamn, Beskrivning, Representant_email) VALUES (?,?,?,?,?,?)"
        cursor.execute(sql, (dataframe[0 + i*8], dataframe[1 + i*8].replace(',' , '.'), dataframe[2 + i*8], dataframe[3 + i*8], dataframe[4 + i*8], dataframe[5 + i*8]))
    
        sql = "INSERT INTO Specialiseringar(Kurskod, Specialisering) VALUES (?,?)"
        cursor.execute(sql, (dataframe[0+i*8], dataframe[7+i*8]))
        
        sql = "INSERT INTO Läsperioder(Kurskod, Läsperiod) VALUES (?,?)"
        cursor.execute(sql, (dataframe[0+i*8], dataframe[6+i*8]))
conn.commit()


#cursor.execute("SELECT SUM(points) FROM course_info")