CREATE TABLE [dbo].[Persona] (
    [IdPersona]              INT               IDENTITY (1, 1) NOT NULL,
    [IdSituacion]            INT               CONSTRAINT [DF_Persona_IdSituacion] DEFAULT ((1)) NOT NULL,
    [NombreAlias]            NVARCHAR (500)    NOT NULL,
    [Edad]                   INT               CONSTRAINT [DF_Persona_Edad] DEFAULT ((0)) NOT NULL,
    [Foto]                   NVARCHAR (1000)   NOT NULL,
    [Ubicacion]              NVARCHAR (500)    NULL,
    [Coordenadas]            [sys].[geography] NOT NULL,
    [IdHospital]             INT               NOT NULL,
    [Notas]                  NVARCHAR (1000)   NULL,
    [Fuente]                 NVARCHAR (500)    NULL,
    [FechaHoraAgregado]      DATETIME          CONSTRAINT [DF_Table_1_FechaHora] DEFAULT (getdate()) NOT NULL,
    [FechaHoraActualizacion] DATETIME          CONSTRAINT [DF_Table_1_UltimaActualizacion] DEFAULT (getdate()) NOT NULL,
    [IdEstatus]              INT               CONSTRAINT [DF_Persona_IdEstatus] DEFAULT ((1)) NOT NULL,
    [FaceId] INT NULL, 
    [Altura] FLOAT NULL, 
    [Anchura] FLOAT NULL, 
    [MargenIzquierdo] FLOAT NULL, 
    [MargenDerecho] FLOAT NULL, 
    CONSTRAINT [PK_Persona] PRIMARY KEY CLUSTERED ([IdPersona] ASC),
    CONSTRAINT [FK_Persona_Hospital] FOREIGN KEY ([IdHospital]) REFERENCES [dbo].[Hospital] ([IdHospital])
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indica la situación de la persona en el registro 1-Encontrado, persona que fue encontrada y busca a familiares 2-Desaparecido persona que es buscada por sus familiares', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Persona', @level2type = N'COLUMN', @level2name = N'IdSituacion';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Nombre Completo en caso de conocerse, Alias si se desconoce', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Persona', @level2type = N'COLUMN', @level2name = N'NombreAlias';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Edad de la persona', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Persona', @level2type = N'COLUMN', @level2name = N'Edad';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'URL al blob que contiene la Foto', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Persona', @level2type = N'COLUMN', @level2name = N'Foto';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Descripcion de la ubicación', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Persona', @level2type = N'COLUMN', @level2name = N'Ubicacion';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'coordenadas geograficas de la ubicacion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Persona', @level2type = N'COLUMN', @level2name = N'Coordenadas';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Hospital donde se encuentra', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Persona', @level2type = N'COLUMN', @level2name = N'IdHospital';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Notas adicionales', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Persona', @level2type = N'COLUMN', @level2name = N'Notas';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'fuente de donde se obtuvo el registro', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Persona', @level2type = N'COLUMN', @level2name = N'Fuente';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora en que se agregó el registro', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Persona', @level2type = N'COLUMN', @level2name = N'FechaHoraAgregado';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha de la ultima actualización', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Persona', @level2type = N'COLUMN', @level2name = N'FechaHoraActualizacion';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Estatus del registro 1 activo para busquedas 2 la persona ya no es buscada 0 desactivado', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Persona', @level2type = N'COLUMN', @level2name = N'IdEstatus';

