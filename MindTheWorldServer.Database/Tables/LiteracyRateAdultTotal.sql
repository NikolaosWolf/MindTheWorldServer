﻿CREATE TABLE [dbo].[LiteracyRateAdultTotal]
(
	[CountryId] INT NOT NULL, 
    [Year] INT NOT NULL, 
    [Value] DECIMAL(10, 6) NULL, 

    CONSTRAINT PK_LRAT_ID PRIMARY KEY ([CountryId], [Year]),
    CONSTRAINT FK_LRAT_CountryId FOREIGN KEY (CountryId) REFERENCES Country(Id)
)
