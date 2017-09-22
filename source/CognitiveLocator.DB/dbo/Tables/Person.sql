CREATE TABLE [dbo].[Person] (
    [IdPerson]              INT               IDENTITY (1, 1) NOT NULL,
    [IsFound]            INT               CONSTRAINT [DF_Persona_IdSituacion] DEFAULT ((1)) NOT NULL,
    [NameAlias]            NVARCHAR (500)    NOT NULL,
    [Age]                   INT               CONSTRAINT [DF_Persona_Edad] DEFAULT ((0)) NOT NULL,
    [Picture]                   NVARCHAR (1000)   NOT NULL,
    [Location]              NVARCHAR (500)    NULL,
    [GeoLocation]            [sys].[geography] NOT NULL,
    [IdHospital]             INT               NOT NULL,
    [Notes]                  NVARCHAR (1000)   NULL,
    [Source]                 NVARCHAR (500)    NULL,
    [CreatedDate]      DATETIME          CONSTRAINT [DF_Table_1_FechaHora] DEFAULT (getdate()) NOT NULL,
    [ModifiedDate] DATETIME          CONSTRAINT [DF_Table_1_UltimaActualizacion] DEFAULT (getdate()) NOT NULL,
    [IsActive]              INT               CONSTRAINT [DF_Persona_IdEstatus] DEFAULT ((1)) NOT NULL,
    [FaceId] INT NULL, 
    [Height] FLOAT NULL, 
    [Width] FLOAT NULL, 
    [LeftMargin] FLOAT NULL, 
    [RightMargin] FLOAT NULL, 
    CONSTRAINT [PK_Persona] PRIMARY KEY CLUSTERED ([IdPerson] ASC),
    CONSTRAINT [FK_Persona_Hospital] FOREIGN KEY ([IdHospital]) REFERENCES [dbo].[Hospital] ([IdHospital])
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indica la situación de la persona en el registro 1-Encontrado, persona que fue encontrada y busca a familiares 2-Desaparecido persona que es buscada por sus familiares', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Person', @level2type = N'COLUMN', @level2name = 'IsFound';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Nombre Completo en caso de conocerse, Alias si se desconoce', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Person', @level2type = N'COLUMN', @level2name = 'NameAlias';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Edad de la persona', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Person', @level2type = N'COLUMN', @level2name = 'Age';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'URL al blob que contiene la Foto', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Person', @level2type = N'COLUMN', @level2name = 'Picture';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Descripcion de la ubicación', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Person', @level2type = N'COLUMN', @level2name = 'Location';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'coordenadas geograficas de la ubicacion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Person', @level2type = N'COLUMN', @level2name = 'GeoLocation';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Hospital donde se encuentra', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Person', @level2type = N'COLUMN', @level2name = N'IdHospital';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Notas adicionales', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Person', @level2type = N'COLUMN', @level2name = 'Notes';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'fuente de donde se obtuvo el registro', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Person', @level2type = N'COLUMN', @level2name = 'Source';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora en que se agregó el registro', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Person', @level2type = N'COLUMN', @level2name = 'CreatedDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha de la ultima actualización', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Person', @level2type = N'COLUMN', @level2name = 'ModifiedDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Estatus del registro 1 activo para busquedas 2 la persona ya no es buscada 0 desactivado', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Person', @level2type = N'COLUMN', @level2name = 'IsActive';

