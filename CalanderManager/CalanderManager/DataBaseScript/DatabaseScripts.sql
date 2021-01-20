CREATE TABLE [dbo].[Calander]
(
    [DateKey]   [INT]  NOT NULL
   ,[FullDate]  [DATE] NOT NULL
   ,[IsHoliday] [BIT]  NOT NULL
        CONSTRAINT [DF_dbo.Calander_IsHoliday]
            DEFAULT ((0))
) ON [PRIMARY] ;
GO
ALTER TABLE [dbo].[Calander]
ADD CONSTRAINT [PK_Calander]
    PRIMARY KEY CLUSTERED ([DateKey]) ON [PRIMARY] ;
GO

CREATE TABLE [dbo].[Holiday]
(
    [DateKey]        [INT]         NOT NULL
   ,[CountryHoliday] [VARCHAR](50) NOT NULL
) ON [PRIMARY] ;
GO
ALTER TABLE [dbo].[Holiday]
ADD CONSTRAINT [PK_Holiday]
    PRIMARY KEY CLUSTERED ([DateKey]) ON [PRIMARY] ;
GO

INSERT INTO dbo.Holiday ([DateKey], [FullDate], [IsHoliday])
VALUES
( 20211225, N'2021-12-25T00:00:00', 0 )

INSERT INTO dbo.Calander ([DateKey], [CountryHoliday])
VALUES
( 20211225, 'US' )
