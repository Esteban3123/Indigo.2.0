CREATE TABLE [dbo].[HCRADDOSIS] (
    [ID]                INT             IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [IDHCRADESQUEMAS]   INT             NOT NULL,
    [FECHAREGISTRO]     DATETIME        NOT NULL,
    [USUARIOREGISTRO]   CHAR (20)       NOT NULL,
    [DOSISTUMOR1]       DECIMAL (18, 2) NULL,
    [DOSISTUMOR2]       DECIMAL (18, 2) NULL,
    [DOSISTUMOR3]       DECIMAL (18, 2) NULL,
    [DOSISTUMOR4]       DECIMAL (18, 2) NULL,
    [DOSISTUMOR5]       DECIMAL (18, 2) NULL,
    [DOSISTUMOR6]       DECIMAL (18, 2) NULL,
    [TOTALDOSIS]        DECIMAL (18, 2) NULL,
    [CODESPECI]         CHAR (3)        NULL,
    [TIEMPOUNIMONITOR]  INT             NULL,
    [OBSERVACION]       VARCHAR (MAX)   NULL,
    [TIPOBRAQUITERAPIA] INT             NULL,
    [VOLUTRATAMIEN]     INT             NULL,
    [IDAGEQUIPTRA]      INT             NULL,
    [DOSISPRESCRITA]    INT             NULL,
    [IDCITA]            INT             NOT NULL,
    [CODCENATEDOSIS]    CHAR (10)       NULL,
    CONSTRAINT [PK_HCRADDOSIS] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_HCRADDOSIS_ADCENATEN] FOREIGN KEY ([CODCENATEDOSIS]) REFERENCES [dbo].[ADCENATEN] ([CODCENATE]),
    CONSTRAINT [FK_HCRADDOSIS_AGASICITA] FOREIGN KEY ([IDCITA]) REFERENCES [dbo].[AGASICITA] ([CODAUTONU]),
    CONSTRAINT [FK_HCRADDOSIS_AGEQUIPTRA] FOREIGN KEY ([IDAGEQUIPTRA]) REFERENCES [dbo].[AGEQUIPTRA] ([ID]),
    CONSTRAINT [FK_HCRADDOSIS_HCRADTUMORES] FOREIGN KEY ([IDHCRADESQUEMAS]) REFERENCES [dbo].[HCRADESQUEMAS] ([ID]),
    CONSTRAINT [FK_HCRADDOSIS_INESPECIA] FOREIGN KEY ([CODESPECI]) REFERENCES [dbo].[INESPECIA] ([CODESPECI])
);


GO
ALTER TABLE [dbo].[HCRADDOSIS] NOCHECK CONSTRAINT [FK_HCRADDOSIS_ADCENATEN];


GO
ALTER TABLE [dbo].[HCRADDOSIS] NOCHECK CONSTRAINT [FK_HCRADDOSIS_AGASICITA];


GO
ALTER TABLE [dbo].[HCRADDOSIS] NOCHECK CONSTRAINT [FK_HCRADDOSIS_AGEQUIPTRA];


GO
ALTER TABLE [dbo].[HCRADDOSIS] NOCHECK CONSTRAINT [FK_HCRADDOSIS_HCRADTUMORES];


GO
ALTER TABLE [dbo].[HCRADDOSIS] NOCHECK CONSTRAINT [FK_HCRADDOSIS_INESPECIA];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Centro de Atención en el cual fue aplicada la dosis de Radioterapia.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRADDOSIS', @level2type = N'COLUMN', @level2name = N'CODCENATEDOSIS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'ID cita medica con la que se registro la dosis', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRADDOSIS', @level2type = N'COLUMN', @level2name = N'IDCITA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '1 - Isocentro  2 - Piel  3  - Isocentro y piel  4  - Puntos A  5 - Mucosa vaginal  6 - Mucosa esofágica', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRADDOSIS', @level2type = N'COLUMN', @level2name = N'DOSISPRESCRITA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del Equipo de Tratamiento', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRADDOSIS', @level2type = N'COLUMN', @level2name = N'IDAGEQUIPTRA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '1 - Primario Solo  2 - Ninguno', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRADDOSIS', @level2type = N'COLUMN', @level2name = N'VOLUTRATAMIEN';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '1 - Intracavitaria  2 - Intraluminal  3 - Intersticial  ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRADDOSIS', @level2type = N'COLUMN', @level2name = N'TIPOBRAQUITERAPIA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Observacion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRADDOSIS', @level2type = N'COLUMN', @level2name = N'OBSERVACION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Tiempo en Minutos Monitor', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRADDOSIS', @level2type = N'COLUMN', @level2name = N'TIEMPOUNIMONITOR';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo de la Especialidad', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRADDOSIS', @level2type = N'COLUMN', @level2name = N'CODESPECI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Total de Dosis que es la sumatoria de todos los tumores', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRADDOSIS', @level2type = N'COLUMN', @level2name = N'TOTALDOSIS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Dosis Aplicada Tumor 6', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRADDOSIS', @level2type = N'COLUMN', @level2name = N'DOSISTUMOR6';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Dosis Aplicada Tumor 5', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRADDOSIS', @level2type = N'COLUMN', @level2name = N'DOSISTUMOR5';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Dosis Aplicada Tumor 4', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRADDOSIS', @level2type = N'COLUMN', @level2name = N'DOSISTUMOR4';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Dosis Aplicada Tumor 3', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRADDOSIS', @level2type = N'COLUMN', @level2name = N'DOSISTUMOR3';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Dosis Aplicada Tumor 2', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRADDOSIS', @level2type = N'COLUMN', @level2name = N'DOSISTUMOR2';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Dosis Aplicada Tumor 1', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRADDOSIS', @level2type = N'COLUMN', @level2name = N'DOSISTUMOR1';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Usuario registro', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRADDOSIS', @level2type = N'COLUMN', @level2name = N'USUARIOREGISTRO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha registro', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRADDOSIS', @level2type = N'COLUMN', @level2name = N'FECHAREGISTRO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id Esquema', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRADDOSIS', @level2type = N'COLUMN', @level2name = N'IDHCRADESQUEMAS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Consecutivo de la tabla', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRADDOSIS', @level2type = N'COLUMN', @level2name = N'ID';

