CREATE TABLE [dbo].[HCSOPORTECACPACIENTE] (
    [ID]                 INT                                                                              IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [IDHCSOPORTECAC]     INT                                                                              NOT NULL,
    [IPCODPACI]          VARCHAR (25) MASKED WITH (FUNCTION = 'partial(0, "Identification_Ofuscado", 0)') NOT NULL,
    [ANIO]               INT                                                                              NOT NULL,
    [FECHACREACION]      DATETIME                                                                         NOT NULL,
    [FECHAFIN]           DATETIME                                                                         NULL,
    [FECHAULTIMOSOPORTE] DATETIME                                                                         NULL,
    [COMPLETO]           BIT                                                                              NOT NULL,
    CONSTRAINT [PK_HCSOPORTECACPACIENTE] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_HCSOPORTECAC_SoporteCAC] FOREIGN KEY ([IDHCSOPORTECAC]) REFERENCES [dbo].[HCSOPORTECAC] ([ID]),
    CONSTRAINT [FK_HCSOPORTECACPACIENTE_INPACIENT] FOREIGN KEY ([IPCODPACI]) REFERENCES [dbo].[INPACIENT] ([IPCODPACI])
);


GO
ADD SENSITIVITY CLASSIFICATION TO
    [dbo].[HCSOPORTECACPACIENTE].[IPCODPACI]
    WITH (LABEL = 'Confidential - PII', INFORMATION_TYPE = 'National ID');


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador BIT (1=Completo/exitoso, 0=incompleto/fallo). Señala si el proceso de generación y soporte CAC del paciente se realizó correctamente.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCSOPORTECACPACIENTE', @level2type = N'COLUMN', @level2name = N'COMPLETO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'1: Completo -  0: no se realizo proceso correctamente ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCSOPORTECACPACIENTE', @level2type = N'COLUMN', @level2name = N'COMPLETO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCSOPORTECACPACIENTE', @level2type = N'COLUMN', @level2name = N'COMPLETO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora del último soporte registrado o actualizado en este proceso del paciente (DATETIME, nullable). Rastrea la última actividad.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCSOPORTECACPACIENTE', @level2type = N'COLUMN', @level2name = N'FECHAULTIMOSOPORTE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'guarda la fecha del ultimo soportes', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCSOPORTECACPACIENTE', @level2type = N'COLUMN', @level2name = N'FECHAULTIMOSOPORTE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCSOPORTECACPACIENTE', @level2type = N'COLUMN', @level2name = N'FECHAULTIMOSOPORTE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora de finalización del proceso de soporte CAC para el paciente (DATETIME, nullable). Indica cuándo culminó la gestión.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCSOPORTECACPACIENTE', @level2type = N'COLUMN', @level2name = N'FECHAFIN';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha fin de proceso por el paciente', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCSOPORTECACPACIENTE', @level2type = N'COLUMN', @level2name = N'FECHAFIN';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCSOPORTECACPACIENTE', @level2type = N'COLUMN', @level2name = N'FECHAFIN';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora de creación del registro de soporte CAC del paciente (DATETIME). Marca de inicio del proceso.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCSOPORTECACPACIENTE', @level2type = N'COLUMN', @level2name = N'FECHACREACION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha creacion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCSOPORTECACPACIENTE', @level2type = N'COLUMN', @level2name = N'FECHACREACION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCSOPORTECACPACIENTE', @level2type = N'COLUMN', @level2name = N'FECHACREACION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Año de generación del soporte CAC (INT). Período fiscal o de gestión del documento.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCSOPORTECACPACIENTE', @level2type = N'COLUMN', @level2name = N'ANIO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Año de generacion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCSOPORTECACPACIENTE', @level2type = N'COLUMN', @level2name = N'ANIO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCSOPORTECACPACIENTE', @level2type = N'COLUMN', @level2name = N'ANIO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código/identificación del paciente (cédula, documento o equivalente). VARCHAR(25) enmascarado como PII. FK a INPACIENT.IPCODPACI.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCSOPORTECACPACIENTE', @level2type = N'COLUMN', @level2name = N'IPCODPACI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Identificacion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCSOPORTECACPACIENTE', @level2type = N'COLUMN', @level2name = N'IPCODPACI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCSOPORTECACPACIENTE', @level2type = N'COLUMN', @level2name = N'IPCODPACI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de la cabecera de soporte CAC (FK a HCSOPORTECAC). Vincula el registro con el soporte principal del Componente de Acreditación en Consulta.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCSOPORTECACPACIENTE', @level2type = N'COLUMN', @level2name = N'IDHCSOPORTECAC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id cabecera de soporte CAC (HCSOPORTECAC)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCSOPORTECACPACIENTE', @level2type = N'COLUMN', @level2name = N'IDHCSOPORTECAC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCSOPORTECACPACIENTE', @level2type = N'COLUMN', @level2name = N'IDHCSOPORTECAC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único autoincremental (INT IDENTITY) de la tabla HCSOPORTECACPACIENTE. Clave primaria.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCSOPORTECACPACIENTE', @level2type = N'COLUMN', @level2name = N'ID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Autonumerico de la tabla', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCSOPORTECACPACIENTE', @level2type = N'COLUMN', @level2name = N'ID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCSOPORTECACPACIENTE', @level2type = N'COLUMN', @level2name = N'ID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Registro de pacientes vinculados a un soporte de Cuenta de Alto Costo (CAC), indicando el período de vigencia, la fecha del último soporte recibido y si el proceso de documentación está completo.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCSOPORTECACPACIENTE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCSOPORTECACPACIENTE';
