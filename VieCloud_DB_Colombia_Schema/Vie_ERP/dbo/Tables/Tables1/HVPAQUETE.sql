CREATE TABLE [dbo].[HVPAQUETE] (
    [ID]          INT                                                                              IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [IDPACSINCRO] INT                                                                              NOT NULL,
    [NUMEFOLIO]   NCHAR (10)                                                                       NULL,
    [CODAUTONU]   INT                                                                              NULL,
    [AUTOLABOR]   INT                                                                              NULL,
    [NUMMUESTRA]  INT                                                                              NULL,
    [IPCODPACI]   VARCHAR (25) MASKED WITH (FUNCTION = 'partial(0, "Identification_Ofuscado", 0)') NULL,
    CONSTRAINT [PK_HVPAQUETE] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_HVPAQUETE_HVSINCRO] FOREIGN KEY ([IDPACSINCRO]) REFERENCES [dbo].[HVSINCRO] ([ID]),
    CONSTRAINT [FK_HVPAQUETE_INPACIENT] FOREIGN KEY ([IPCODPACI]) REFERENCES [dbo].[INPACIENT] ([IPCODPACI])
);


GO
ADD SENSITIVITY CLASSIFICATION TO
    [dbo].[HVPAQUETE].[IPCODPACI]
    WITH (LABEL = 'Confidential - PII', INFORMATION_TYPE = 'National ID');



GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del paciente (cédula, identificación, documento). PII ofuscado. FK a INPACIENT. VARCHAR(25), máscara Identification_Ofuscado.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HVPAQUETE', @level2type = N'COLUMN', @level2name = N'IPCODPACI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo del paciente', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HVPAQUETE', @level2type = N'COLUMN', @level2name = N'IPCODPACI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HVPAQUETE', @level2type = N'COLUMN', @level2name = N'IPCODPACI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número de muestra de laboratorio o especimen. Identificador único del laboratorio para análisis, examen o prueba diagnóstica. INT.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HVPAQUETE', @level2type = N'COLUMN', @level2name = N'NUMMUESTRA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Numero de muestra', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HVPAQUETE', @level2type = N'COLUMN', @level2name = N'NUMMUESTRA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HVPAQUETE', @level2type = N'COLUMN', @level2name = N'NUMMUESTRA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'ID autonumérico del laboratorio. Referencia interna al laboratorio o unidad funcional responsable del procesamiento de muestras. INT.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HVPAQUETE', @level2type = N'COLUMN', @level2name = N'AUTOLABOR';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'ID Autonumérico del laboratorio', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HVPAQUETE', @level2type = N'COLUMN', @level2name = N'AUTOLABOR';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HVPAQUETE', @level2type = N'COLUMN', @level2name = N'AUTOLABOR';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código autonumérico de cita médica. Se registra si el paquete corresponde a una atención o cita médica programada. INT.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HVPAQUETE', @level2type = N'COLUMN', @level2name = N'CODAUTONU';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'En caso de que sea cita médica, se guarda el código del autonumerico ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HVPAQUETE', @level2type = N'COLUMN', @level2name = N'CODAUTONU';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HVPAQUETE', @level2type = N'COLUMN', @level2name = N'CODAUTONU';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número del folio de historia clínica. Se registra si el paquete corresponde a un registro de historia clínica o documento de atención. NCHAR(10).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HVPAQUETE', @level2type = N'COLUMN', @level2name = N'NUMEFOLIO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'En caso de que sea Historia, se guarda el numero del folio', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HVPAQUETE', @level2type = N'COLUMN', @level2name = N'NUMEFOLIO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HVPAQUETE', @level2type = N'COLUMN', @level2name = N'NUMEFOLIO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'ID de sincronización PAC (HVSINCRO). Referencia a pacientes sincronizados del sistema. FK a HVSINCRO. INT.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HVPAQUETE', @level2type = N'COLUMN', @level2name = N'IDPACSINCRO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de la tabla HVSINCRO, que contiene los pacientes sincronizados', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HVPAQUETE', @level2type = N'COLUMN', @level2name = N'IDPACSINCRO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HVPAQUETE', @level2type = N'COLUMN', @level2name = N'IDPACSINCRO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Autonumérico (clave primaria) de la tabla HVPAQUETE. Identificador único del paquete de atención o laboratorio. INT IDENTITY.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HVPAQUETE', @level2type = N'COLUMN', @level2name = N'ID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Autonumerico tabla', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HVPAQUETE', @level2type = N'COLUMN', @level2name = N'ID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HVPAQUETE', @level2type = N'COLUMN', @level2name = N'ID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Registro de paquetes o lotes de muestras de laboratorio asociados a pacientes, que agrupa folios, autorizaciones y números de muestra para el procesamiento y seguimiento de órdenes de laboratorio.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HVPAQUETE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HVPAQUETE';
