# Structure of this folder
The structure of this folder should be able to be mapped into database by the migration code.
```
mapline
    data
    |   DATA-README.md
    |
    └─── {database-table}
        |
        |
        └─── {name}
              |
              |
              └─── {year}
                      area.geojson
                      features.json
                    
```

## database-table
The name of the database table. Right now it is only `Language`

## name
The name of this structure represents the Name column of the database table.

## year
The name of this structure represents the Name column of the database table.
The format is following {startYear}-{EndYear}
If the year is less than 0, then the year must end with BCE

### Example
Start year: -2500 (2500 BCE)
End year: 1500 (1500 CE)
then, the name of the folder: `2500BCE-1500`

## area.geojson
Represents the the area column of the database table in GEOJSON format

## features.json
Represents the features column of the databse table
