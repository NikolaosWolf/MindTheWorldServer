﻿CREATE TABLE [dbo].[HappinessScore]
(
	[CountryId] INT NOT NULL , 
    [Year] INT NOT NULL, 
    [Value] DECIMAL(10, 4) NULL, 

    CONSTRAINT PK_ID PRIMARY KEY ([CountryId], [Year]),
    CONSTRAINT FK_CountryId FOREIGN KEY (CountryId) REFERENCES Country(Id)
)