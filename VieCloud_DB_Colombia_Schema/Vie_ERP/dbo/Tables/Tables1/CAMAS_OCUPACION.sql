CREATE TABLE [dbo].[CAMAS_OCUPACION] (
    [CODICAMAS]         INT           NULL,
    [FECINIEST]         DATETIME      NULL,
    [FECFINEST]         DATETIME      NULL,
    [NUMINGRES]         CHAR (10)     NULL,
    [NUMCAMHOS]         CHAR (10)     NULL,
    [UFUCODIGO]         CHAR (20)     NULL,
    [UFUDESCRI]         CHAR (60)     NULL,
    [ESTADO]            CHAR (20)     NULL,
    [ANO]               CHAR (4)      NULL,
    [MES]               CHAR (2)      NULL,
    [DIA]               CHAR (2)      NULL,
    [FECHA_COMPLETA]    DATETIME      NULL,
    [IPTIPODOC]         CHAR (20)     NULL,
    [IPCODPACI]         VARCHAR (25)  NULL,
    [IPNOMCOMP]         CHAR (250)    NULL,
    [FECHA NACIMIENTO]  DATETIME      NULL,
    [EDAD]              INT           NULL,
    [CODIGO ENTIDAD]    VARCHAR (20)  NULL,
    [ENTIDAD]           VARCHAR (300) NULL,
    [CODIGO GRUPO]      VARCHAR (20)  NULL,
    [GRUPO DE ATENCION] VARCHAR (300) NULL,
    [CIE10]             CHAR (4)      NULL,
    [DIAGNOSTICO]       CHAR (350)    NULL,
    [AMBITO]            VARCHAR (15)  NULL
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Se guarda el ambito', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CAMAS_OCUPACION', @level2type = N'COLUMN', @level2name = N'AMBITO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Se guarda el diagnóstico', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CAMAS_OCUPACION', @level2type = N'COLUMN', @level2name = N'DIAGNOSTICO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'se guarda la Clasificación Internacional de Enfermedades', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CAMAS_OCUPACION', @level2type = N'COLUMN', @level2name = N'CIE10';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Se guarda el grupo de atención', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CAMAS_OCUPACION', @level2type = N'COLUMN', @level2name = N'GRUPO DE ATENCION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Se guarda el codigo del grupo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CAMAS_OCUPACION', @level2type = N'COLUMN', @level2name = N'CODIGO GRUPO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Se guarda el nombre de la entidad', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CAMAS_OCUPACION', @level2type = N'COLUMN', @level2name = N'ENTIDAD';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Se guarda el código de la entidad', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CAMAS_OCUPACION', @level2type = N'COLUMN', @level2name = N'CODIGO ENTIDAD';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Se guarda la edad', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CAMAS_OCUPACION', @level2type = N'COLUMN', @level2name = N'EDAD';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Se guarda la fecha de nacimiento', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CAMAS_OCUPACION', @level2type = N'COLUMN', @level2name = N'FECHA NACIMIENTO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Se guarda el nombre completo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CAMAS_OCUPACION', @level2type = N'COLUMN', @level2name = N'IPNOMCOMP';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Se guarda el código del paciente', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CAMAS_OCUPACION', @level2type = N'COLUMN', @level2name = N'IPCODPACI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Se guarda el tipo de documento', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CAMAS_OCUPACION', @level2type = N'COLUMN', @level2name = N'IPTIPODOC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'se guarda la fecha completa', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CAMAS_OCUPACION', @level2type = N'COLUMN', @level2name = N'FECHA_COMPLETA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Se guarda el dia', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CAMAS_OCUPACION', @level2type = N'COLUMN', @level2name = N'DIA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'se guarda el mes', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CAMAS_OCUPACION', @level2type = N'COLUMN', @level2name = N'MES';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Se guarda el año', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CAMAS_OCUPACION', @level2type = N'COLUMN', @level2name = N'ANO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Se guarda el estado', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CAMAS_OCUPACION', @level2type = N'COLUMN', @level2name = N'ESTADO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Se guarda el nombre de la unidad funcional', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CAMAS_OCUPACION', @level2type = N'COLUMN', @level2name = N'UFUDESCRI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Se guarda el código de la unidad funcional', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CAMAS_OCUPACION', @level2type = N'COLUMN', @level2name = N'UFUCODIGO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Se guarda el número de cama hospitalaria', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CAMAS_OCUPACION', @level2type = N'COLUMN', @level2name = N'NUMCAMHOS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Se guarda el número de ingreso', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CAMAS_OCUPACION', @level2type = N'COLUMN', @level2name = N'NUMINGRES';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'se guara la fecha hora final', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CAMAS_OCUPACION', @level2type = N'COLUMN', @level2name = N'FECFINEST';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Se guarda la fecha hora inicial', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CAMAS_OCUPACION', @level2type = N'COLUMN', @level2name = N'FECINIEST';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código de la cama ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CAMAS_OCUPACION', @level2type = N'COLUMN', @level2name = N'CODICAMAS';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Registra la ocupación de camas hospitalarias por estancia, almacenando el período de uso (fecha/hora inicial y final), la cama y unidad funcional asignadas, y el número de ingreso del paciente. Incluye datos desnormalizados del paciente (identificación, nombre, edad, fecha de nacimiento, entidad aseguradora y grupo de atención) junto con el diagnóstico CIE-10 y el ámbito de atención, lo que sugiere uso como tabla de reporte o cargue periódico para análisis de ocupación hospitalaria.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'CAMAS_OCUPACION';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DescriptionSource', @value=N'ai_claude-sonnet-4-6_2026-05-05', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'CAMAS_OCUPACION';
GO
