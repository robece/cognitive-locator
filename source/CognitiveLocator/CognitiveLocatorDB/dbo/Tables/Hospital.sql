CREATE TABLE [dbo].[Hospital] (
    [IdHospital]  INT               IDENTITY (1, 1) NOT NULL,
    [Hospital]    NVARCHAR (500)    NOT NULL,
    [Estado]      NVARCHAR (50)     NOT NULL,
    [Dirección]   NVARCHAR (500)    NULL,
    [Coordenadas] [sys].[geography] NULL,
    CONSTRAINT [PK_Hospital] PRIMARY KEY CLUSTERED ([IdHospital] ASC)
);

