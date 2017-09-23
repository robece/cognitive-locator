CREATE TABLE [dbo].[Hospital] (
    [IdHospital]  INT               IDENTITY (1, 1) NOT NULL,
    [HospitalName]    NVARCHAR (500)    NOT NULL,
    [State]      NVARCHAR (50)     NOT NULL,
    [Address]   NVARCHAR (500)    NULL,
    [GeoLocation] [sys].[geography] NULL,
    CONSTRAINT [PK_Hospital] PRIMARY KEY CLUSTERED ([IdHospital] ASC)
);

