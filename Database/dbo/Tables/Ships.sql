CREATE TABLE [dbo].[Ships]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [Name] VARCHAR(MAX) NOT NULL, 
    [Hp] INT NOT NULL, 
    [FlagColor] INT NOT NULL, 
    [IsYourTurn] BIT NOT NULL,

     
)
