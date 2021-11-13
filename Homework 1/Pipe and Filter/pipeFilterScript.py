import json
import pandas as pd

# Функција за отворање на JSON фајлот и исчитување на податоците како raw data.
def openFile(path):
    with open(path, 'r', encoding='UTF-8') as myfile:
        data = myfile.read()
        return data

# Функција за парсирање на JSON фајлот и изолирање само на оние атрибути кои ни се потребни.
def parseJSONFromFile(data, relevantAttribute):
    jsonData = json.loads(data)
    elements = jsonData[relevantAttribute]
    return elements

# Функција за филтрирање на податоците според вредноста на одреден атрибут.
def filterElementsByValueOfAttribute(elements, attribute, value):
    filteredElements = filter(lambda element: element[attribute] == value, elements)
    filteredElements=list(filteredElements)
    return filteredElements

# Функција за филтрирање на податоците според тоа дали елементите имаат одреден атрибут.
def filterElementsByPresenceOfAttribute(elements, attribute):
    filteredElements = filter(lambda element: attribute in element, elements)
    filteredElements=list(filteredElements)
    return filteredElements

# Функција која креира DataFrame врз основа на атрибутите кои ни се потребни.
# Оваа функција, за разлика од останатите, е многу специфична врз основа на податоците кои ги имаме,
# па затоа подобро е да се изменува по потреба, отколку да се автоматизира целосно.
def createDataFrame(filteredElements):
    ids=[]
    names=[]
    lattitudes=[]
    longitudes=[]
    fees=[]
    for e in filteredElements:
        ids.append(e["id"])
        names.append(e["tags"]["name"])
        lattitudes.append(e["lat"])
        longitudes.append(e["lon"])
        fees.append(e["tags"]["fee"])
    df=pd.DataFrame({'ID':ids,'Name':names,'Fee':fees,'Lattitude':lattitudes,'Longitude':longitudes})
    return df

# Функција која го зачувува DataFrame-от локално на дадената патека во .csv формат.
def saveCsvFile(df, path):
    df.to_csv(path, index=False, encoding="utf-8-sig")


# Главниот тек на програмата започнува тука
print("Програма за трансформација на JSON податоци во CSV формат")

print("Внесете ја апсолутната патека на изворната JSON датотека:")
inputPath=input()
data=openFile(inputPath)

print("Внесете кој атрибут од JSON фајлот ги содржи податоците кои ви се потребни:")
relevantAttribute=input()
elements=parseJSONFromFile(data, relevantAttribute)

print("Започнува процесот на филтрирање на податоците.")
while(True):
    print("Доколку сакате да ги филтрирате податоците според вредноста на одреден атрибут, внесете 1")
    print("Доколку сакате да ги филтрирате податоците според тоа дали содржат одреден атрибут, внесете 2")
    print("Доколку сакате да завршите со филтрирањето на податоците, внесете 3")
    result=input()
    if(result=='1'):
        print("Одбравте филтрирање според вредност на атрибут")
        print("Внесете го името на атрибутот според кој сакате да филтрирате:")
        name=input()
        print("Внесете ја вредноста на атрибутот според која сакате да филтрирате:")
        value=input()
        elements = filterElementsByValueOfAttribute(elements, name, value)
    elif(result=='2'):
        print("Одбравте филтрирање според присуство на атрибут")
        print("Внесете го името на атрибутот чие присуство сакате да го исфилтрирате:")
        name = input()
        elements=filterElementsByPresenceOfAttribute(elements,name)
    elif(result=='3'):
        print("Процесот за филтрирање на податоците е завршен.")
        break
    else:
        print("Невалидна опција! Обидете се повторно.")

dataFrame=createDataFrame(elements)
print("Внесете ја апсолутната патека каде сакате да го зачувате резултантната .csv датотека:")
outputPath=input()
print("Внесете како сакате да се именува резултантната датотека")
name=input()
saveCsvFile(dataFrame,outputPath+"/"+name+".csv")
print("Процесот е успешно завршен")


