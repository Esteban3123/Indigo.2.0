CREATE TABLE [dbo].[ADINTERFA] (
    [CONSECUTI] NUMERIC (18)                                                                     IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [IPCODPACI] VARCHAR (25) MASKED WITH (FUNCTION = 'partial(0, "Identification_Ofuscado", 0)') NOT NULL,
    [NUMINGRES] CHAR (10)                                                                        NOT NULL,
    [NUMCITDGH] VARCHAR (50)                                                                     NULL,
    [NUMORDSER] CHAR (10)                                                                        NOT NULL,
    [INTFECHAD] DATETIME                                                                         NOT NULL,
    [INTUSUARI] CHAR (20)                                                                        NOT NULL,
    [INTUSUIND] CHAR (20)                                                                        NOT NULL,
    [INTUSUWIN] CHAR (60)                                                                        NOT NULL,
    [INTESTRED] CHAR (60)                                                                        NOT NULL,
    [INDAUDFOR] NUMERIC (18)                                                                     NOT NULL,
    CONSTRAINT [PK_ADINTERFA] PRIMARY KEY CLUSTERED ([CONSECUTI] ASC),
    CONSTRAINT [FK_ADINTERFA_ADINGRESO] FOREIGN KEY ([NUMINGRES]) REFERENCES [dbo].[ADINGRESO] ([NUMINGRES]),
    CONSTRAINT [FK_ADINTERFA_INPacient] FOREIGN KEY ([IPCODPACI]) REFERENCES [dbo].[INPACIENT] ([IPCODPACI])
);


GO
ADD SENSITIVITY CLASSIFICATION TO
    [dbo].[ADINTERFA].[IPCODPACI]
    WITH (LABEL = 'Confidential - PII', INFORMATION_TYPE = 'National ID');


GO
CREATE NONCLUSTERED INDEX [IX_ADINTERFA_IPCODPACI]
    ON [dbo].[ADINTERFA]([IPCODPACI] ASC);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código de auditoría (NUMERIC 18), identificador único del evento de auditoría registrado en la interfaz', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADINTERFA', @level2type = N'COLUMN', @level2name = N'INDAUDFOR';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo de Auditoria', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADINTERFA', @level2type = N'COLUMN', @level2name = N'INDAUDFOR';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADINTERFA', @level2type = N'COLUMN', @level2name = N'INDAUDFOR';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Estación de trabajo (CHAR 60), computadora o terminal desde la cual se creó el documento/registro', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADINTERFA', @level2type = N'COLUMN', @level2name = N'INTESTRED';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Estacion de Trabajo desde donde se creo el documento', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADINTERFA', @level2type = N'COLUMN', @level2name = N'INTESTRED';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADINTERFA', @level2type = N'COLUMN', @level2name = N'INTESTRED';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Usuario de Windows (CHAR 60), credencial del sistema operativo del usuario que creó el documento', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADINTERFA', @level2type = N'COLUMN', @level2name = N'INTUSUWIN';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Usuario de Windows que creo el documento', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADINTERFA', @level2type = N'COLUMN', @level2name = N'INTUSUWIN';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADINTERFA', @level2type = N'COLUMN', @level2name = N'INTUSUWIN';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Usuario de Indigo (CHAR 20), credencial de usuario del ERP/EHR Indigo Vie Cloud que creó el registro', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADINTERFA', @level2type = N'COLUMN', @level2name = N'INTUSUIND';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Usuario de Indigo que creo el Registro', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADINTERFA', @level2type = N'COLUMN', @level2name = N'INTUSUIND';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADINTERFA', @level2type = N'COLUMN', @level2name = N'INTUSUIND';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Usuario DGH (CHAR 20), credencial o identificador del usuario del sistema DGH que creó el documento', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADINTERFA', @level2type = N'COLUMN', @level2name = N'INTUSUARI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Usuario DGH que creo el documento', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADINTERFA', @level2type = N'COLUMN', @level2name = N'INTUSUARI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADINTERFA', @level2type = N'COLUMN', @level2name = N'INTUSUARI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha de creación del documento (DATETIME), marca temporal del momento en que se generó el registro en la interfaz', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADINTERFA', @level2type = N'COLUMN', @level2name = N'INTFECHAD';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha de Creacion del Documento', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADINTERFA', @level2type = N'COLUMN', @level2name = N'INTFECHAD';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADINTERFA', @level2type = N'COLUMN', @level2name = N'INTFECHAD';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número de orden de servicio (CHAR 10), identificador de la orden asistencial o servicio de salud asociado', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADINTERFA', @level2type = N'COLUMN', @level2name = N'NUMORDSER';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Numero Orden de Servicio', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADINTERFA', @level2type = N'COLUMN', @level2name = N'NUMORDSER';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADINTERFA', @level2type = N'COLUMN', @level2name = N'NUMORDSER';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Consecutivo de la Cita', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADINTERFA', @level2type = N'COLUMN', @level2name = N'NUMCITDGH';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número de ingreso (CHAR 10), identificador del episodio de atención/hospitalización del paciente (FK→ADINGRESO)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADINTERFA', @level2type = N'COLUMN', @level2name = N'NUMINGRES';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Numero de Ingreso', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADINTERFA', @level2type = N'COLUMN', @level2name = N'NUMINGRES';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADINTERFA', @level2type = N'COLUMN', @level2name = N'NUMINGRES';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del paciente (VARCHAR 25, PII ofuscado), identificación del paciente: cédula, documento o identificación sanitaria (FK→INPACIENT)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADINTERFA', @level2type = N'COLUMN', @level2name = N'IPCODPACI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo del Paciente', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADINTERFA', @level2type = N'COLUMN', @level2name = N'IPCODPACI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADINTERFA', @level2type = N'COLUMN', @level2name = N'IPCODPACI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Consecutivo de registro (NUMERIC 18, Identity), identificador único auto-incremental de cada evento registrado en la interfaz', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADINTERFA', @level2type = N'COLUMN', @level2name = N'CONSECUTI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Consecutivo de registro  Identificador: 00000010', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADINTERFA', @level2type = N'COLUMN', @level2name = N'CONSECUTI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADINTERFA', @level2type = N'COLUMN', @level2name = N'CONSECUTI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Registro de interfaz o integración entre admisiones y órdenes de servicios: almacena la trazabilidad de las transacciones enviadas o recibidas por el sistema de interfaz (DGH u otros), vinculando paciente, ingreso y orden de servicio con el usuario y equipo que ejecutó la operación.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADINTERFA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADINTERFA';
