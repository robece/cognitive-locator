CREATE TABLE [dbo].[PersonFaces]
(
	[IdPersonaFaces] INT NOT NULL , 
    [idPersona] INT NOT NULL, 
    [URL] NVARCHAR(500) NOT NULL, 
    [FaceId] INT NULL, 
	[Altura] FLOAT NULL, 
    [Anchura] FLOAT NULL, 
    [MargenIzquierdo] FLOAT NULL, 
    [MargenDerecho] FLOAT NULL, 
    CONSTRAINT [PK_PersonaFaces] PRIMARY KEY ([IdPersonaFaces] ASC), 
    CONSTRAINT [FK_PersonFaces_Person] FOREIGN KEY ([idPersona]) REFERENCES [dbo].[Persona]([IdPersona]),

)
