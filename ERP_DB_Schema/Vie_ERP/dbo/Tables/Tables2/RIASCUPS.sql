CREATE TABLE [dbo].[RIASCUPS] (
    [ID]               INT         IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [CODSERIPS]        CHAR (20)   NOT NULL,
    [IDRIAS]           INT         NOT NULL,
    [CONCEPTORIPSRIAS] VARCHAR (2) NOT NULL,
    CONSTRAINT [PK_RIASCUPS] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_RIASCUPS_INCUPSIPS] FOREIGN KEY ([CODSERIPS]) REFERENCES [dbo].[INCUPSIPS] ([CODSERIPS]),
    CONSTRAINT [FK_RIASCUPS_RIAS] FOREIGN KEY ([IDRIAS]) REFERENCES [dbo].[RIAS] ([ID])
);


GO
ALTER TABLE [dbo].[RIASCUPS] NOCHECK CONSTRAINT [FK_RIASCUPS_INCUPSIPS];


GO
ALTER TABLE [dbo].[RIASCUPS] NOCHECK CONSTRAINT [FK_RIASCUPS_RIAS];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Finalidad de la consulta:    01 = Accidente de trabajo (AC)  02 = Accidente de tránsito (AC)  03 = Accidente rábico  (AC)  04 = Accidente ofídico  (AC)  05 = Otro tipo de accidente  (AC)  06 = Evento catastrófico  (AC)  07 = Lesión por agresión  (AC)  08 = Lesión auto infligida  (AC)  09 = Sospecha de maltrato físico  (AC)  10 = Sospecha de abuso sexual  (AC)  11 = Sospecha de violencia sexual  (AC)  12 = Sospecha de maltrato emocional  (AC)  13 = Enfermedad general  (AC)  14 = Enfermedad laboral  (AC)  15 = Otra  (AC)    Finalidad del procedimiento:  16 = Diagnóstico (AP)  17 = Terapéutico  (AP)  18 = Protección específica (AP)  19 = Detección temprana de enfermedad general (AP)  20 = Detección temprana de enfermedad laboral (AP)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RIASCUPS', @level2type = N'COLUMN', @level2name = N'CONCEPTORIPSRIAS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'ID de la tabla de RIAS (RIAS)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RIASCUPS', @level2type = N'COLUMN', @level2name = N'IDRIAS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo del Servicio IPS (Tabla INCUPSIS)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RIASCUPS', @level2type = N'COLUMN', @level2name = N'CODSERIPS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Autonumerico de la tabla', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RIASCUPS', @level2type = N'COLUMN', @level2name = N'ID';

