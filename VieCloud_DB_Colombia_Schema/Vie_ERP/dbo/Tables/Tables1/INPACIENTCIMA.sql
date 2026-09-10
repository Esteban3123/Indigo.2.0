CREATE TABLE [dbo].[INPACIENTCIMA] (
    [IPCODPACI]      VARCHAR (30) NOT NULL,
    [ESTADO_CIVIL]   CHAR (30)    NOT NULL,
    [Idioma]         CHAR (30)    NOT NULL,
    [RELIGION]       CHAR (30)    NOT NULL,
    [PaisResidencia] CHAR (50)    NOT NULL,
    [Provincia]      CHAR (50)    NOT NULL,
    [Canton]         CHAR (50)    NOT NULL,
    [Distrito]       CHAR (50)    NOT NULL,
    [Barrio]         CHAR (50)    NOT NULL,
    [EMAIL]          CHAR (100)   NOT NULL,
    [Cliente]        CHAR (30)    NOT NULL
);
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Tabla de datos sociodemográficos y de contacto complementarios de pacientes, vinculada a la entidad principal de pacientes mediante un código de paciente. Almacena información como estado civil, idioma, religión, dirección geográfica detallada (país, provincia, cantón, distrito y barrio), correo electrónico y el cliente al que pertenece el registro, sugiriendo uso en entornos multitenant.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'INPACIENTCIMA';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DescriptionSource', @value=N'ai_claude-sonnet-4-6_2026-05-05', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'INPACIENTCIMA';
GO
