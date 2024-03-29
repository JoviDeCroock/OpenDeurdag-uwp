# OpenDeurdag-uwp

## Leden:

Callens Elien

De Croock Jovi

Schuddinck Kevin


## Inhoudstafel:

| Hoofdstuk                                                            |
|:--------------------------------------------------------------------:|
|[Notities](https://github.com/NastyDipster/OpenDeurdag-uwp#notities)   |
|[Objects](https://github.com/NastyDipster/OpenDeurdag-uwp#objecten)   |
|[Methodes](https://github.com/NastyDipster/OpenDeurdag-uwp#methodes)  |
|[Client](https://github.com/NastyDipster/OpenDeurdag-uwp#client)      |


## API

### Notities

Probleem: veel op veel relatie Student Campus Trainingen

Onze oplossing: 

1. Uitwerking in database

2. Methods initieel testen

3. JsonIgnore van: 

	Trainingen -> Studenten

	Trainingen -> Campussen

	Campussen -> Studenten

	Reden: Oneindige GET methodes --> Crash

Bij Gebruik: open Package Manager Console en voer het commando "Update-Database" uit
Check daarna of eerst RondleidingsAPI opgestart word in solution settings, daarna kunt u runnen.



### Objecten

#### User

| Varnaam       | Type          | 
|:-------------:|:-------------:| 
| Name          | string        | 
| Email         | string        | 


#### Admin (inherited van user)

| Varnaam       | Type          | 
|:-------------:|:-------------:| 
| AdminId       | integer       | 
| Password      | encodedString | 


#### Student (inherited van user)

| Varnaam        | Type          | 
|:-------------: |:-------------:| 
| StudentId      | integer       | 
| Street         | string        | 
| HouseNumber    |string(vb 29A) | 
| City           | string        | 
| Telephone      | string        | 
| Feed           | string        | 
| Campussen      | CampusList    | 
| Students       | StudentList   | 



#### Campus

| Varnaam       | Type          | 
|:-------------:|:-------------:| 
| CampusId      | integer       | 
| Name          | string        | 
| Street        | string        |    
| HouseNumber   |string (vb 29A)| 
| City          | string        | 
| Telephone     | string        | 
| Feed          | string        |  
| Trainingen    | TrainingList  | 
| Students      | StudentList   | 


#### Training

| Varnaam       | Type          | 
|:-------------:|:-------------:| 
| TrainingId    | integer       | 
| Name          | string        | 
| Description   | string        | 
| Campussen     | CampusList    | 
| Students      | StudentList   | 

#### Post (Custom posts door admin)

| Varnaam       | Type          | 
|:-------------:|:-------------:| 
| PostId        | integer       | 
| Title         | string        | 
| Body          | string        | 
| Date          | DateTime      | 


### Methodes

#### Training

| Method           | Type          | Return            |
|:----------------:|:-------------:|:-----------------:| 
| api/Trainings    | Get           |  all trainings    |
| api/Trainings/id | Get           | specific training |


#### Posts

| Method           | Type          | Return            |
|:----------------:|:-------------:|:-----------------:| 
| api/Posts        | Get           |  all Posts        |
| api/Posts/id     | Get           | specific Post     |
| api/Posts/       | Post          | createPost        |
| api/Posts/id     | Delete        | delete Post       |


#### Campus

| Method           | Type          | Return            |
|:----------------:|:-------------:|:-----------------:| 
| api/Campus       | Get           |  all Campusses    |
| api/Campus/id    | Get           | specific Campus   |


#### Admin

| Method           | Type          | Return            |
|:----------------:|:-------------:|:-----------------:| 
| api/Admins       | Get           |  all Admins       |
| api/Admins/id    | Get           | specific Admin    |
| api/Admins       | Post          | login method      |


#### Student

| Method           | Type          | Return            |
|:----------------:|:-------------:|:-----------------:| 
| api/Students     | Get           |  all Students     |
| api/Students/id  | Get           | specific Student  |
| api/Students     | Post          | CreateStudent     |


## Client

### Doel

Wij doelen op support voor de Windows Phone en voor Windows Desktops


### Functionaliteiten

1. Facebook Feeds bekijken van de Campussen en Trainingen (Keuze welke ook), alsook de Posts van Admins
2. Routebegeleiding naar de gekozen Campus
3. Een voorkeur qua Training en Campus doorgeven aan de HoGent
4. Admin:

	- CRUD van Posts

	-Bekijken voorkeur Trainings en Campussen studenten --> deze ook exporteren naar .csv
