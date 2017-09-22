CREATE TABLE [dbo].[PersonFaces]
(
	[IdPersonFaces] INT NOT NULL , 
    [idPerson] INT NOT NULL, 
    [URL] NVARCHAR(500) NOT NULL, 
    [FaceId] INT NULL, 
	[Height] FLOAT NULL, 
    [Width] FLOAT NULL, 
    [LeftMargin] FLOAT NULL, 
    [RightMargin] FLOAT NULL, 
    CONSTRAINT [PK_PersonaFaces] PRIMARY KEY ([IdPersonFaces] ASC), 
    CONSTRAINT [FK_PersonFaces_Person] FOREIGN KEY ([idPerson]) REFERENCES [dbo].[Persona]([IdPerson]),

)
