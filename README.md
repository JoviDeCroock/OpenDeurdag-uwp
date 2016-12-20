# OpenDeurdag-uwp

## Leden:

Callens Elien.
De Croock Jovi.
Schuddinck Kevin.

## Inhoudstafel:

| Hoofdstuk                                                                                |
|:----------------------------------------------------------------------------------------:|
|[Objects](https://github.com/HoGentTIN/projecten-3-g_st_di_1100/tree/WebDevS3#Objecten)   |
|[Methodes](https://github.com/HoGentTIN/projecten-3-g_st_di_1100/tree/WebDevS3#Methodes)  |
|[Client](https://github.com/HoGentTIN/projecten-3-g_st_di_1100/tree/WebDevS3#Client)      |


## API

### Notities

Probleem: veel op veel relatie Student Campus Trainingen.
Onze oplossing: .
1. Uitwerking in database.
2. Methods initieel testen.
3. JsonIgnore van: .
	Trainingen -> Studenten.
	Trainingen -> Campussen.
	Campussen -> Studenten.
	Reden: Oneindige GET methodes --> Crash.


### Objecten

#### User

| Varnaam       | Type         | 
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
| api/Studnets     | Get           |  all Students     |
| api/Students/id  | Get           | specific Student  |
| api/Students     | Post          | CreateStudent     |


## Client
