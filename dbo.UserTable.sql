CREATE TABLE [dbo].[User]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [UserId] NVARCHAR(30) NULL, 
    [Password] NVARCHAR(30) NULL, 
    [FirstName] NVARCHAR(50) NULL, 
    [LastName] NVARCHAR(50) NULL, 
    [PhoneNumber] NVARCHAR(30) NULL, 
    [EmailAddress] NVARCHAR(50) NULL, 
    [Gender] SMALLINT NULL, 
    [TermCondition] BIT NULL, 
    [CreationDate] DATETIME NULL,
	CONSTRAINT CK_Gender CHECK (Gender = 1 or Gender = 2) 
)
