CREATE TABLE [dbo].[HCHOJAMED] (
    [CONSECUTI]      NUMERIC (18)                                                                     IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [IPCODPACI]      VARCHAR (25) MASKED WITH (FUNCTION = 'partial(0, "Identification_Ofuscado", 0)') NOT NULL,
    [NUMINGRES]      CHAR (10)                                                                        NOT NULL,
    [CODCENATE]      CHAR (10)                                                                        NOT NULL,
    [UFUCODIGO]      CHAR (10)                                                                        NOT NULL,
    [CODPROSAL]      CHAR (20) MASKED WITH (FUNCTION = 'partial(0, "Identification_Ofuscado", 0)')    NOT NULL,
    [CODPROAPL]      CHAR (20)                                                                        NULL,
    [CODPRODUC]      CHAR (20)                                                                        NOT NULL,
    [FECINITRA]      DATETIME                                                                         NOT NULL,
    [FECPROAPL]      DATETIME                                                                         NOT NULL,
    [FECAPLMED]      DATETIME                                                                         NULL,
    [FECREGSIS]      DATETIME                                                                         CONSTRAINT [DF_HCENF_FECREGSIS] DEFAULT ([Common].[getdate]()) NOT NULL,
    [CODVIAADM]      CHAR (20)                                                                        NOT NULL,
    [DOSISPROD]      NUMERIC (18, 2)                                                                  NULL,
    [CODUNIMED]      CHAR (30)                                                                        NULL,
    [FRECUENCI]      INT                                                                              NULL,
    [UNIFRECUE]      CHAR (1)                                                                         NULL,
    [DURACIDOS]      CHAR (20)                                                                        NOT NULL,
    [VALDURFIJ]      INT                                                                              NULL,
    [UNIDURFIJ]      CHAR (1)                                                                         NULL,
    [FECFINDOS]      DATETIME                                                                         NULL,
    [MEDESTADO]      CHAR (1)                                                                         NOT NULL,
    [CABESTADO]      CHAR (1)                                                                         NOT NULL,
    [INDAPLMED]      VARCHAR (MAX)                                                                    NULL,
    [MOTSUSMED]      VARCHAR (2000)                                                                   NULL,
    [CODUSUSUS]      CHAR (20)                                                                        NULL,
    [CANDESCON]      INT                                                                              NULL,
    [FORMUMANU]      BIT                                                                              NOT NULL,
    [DESADMINI]      VARCHAR (MAX)                                                                    NULL,
    [CODTIPEST]      CHAR (3)                                                                         NULL,
    [CONSECPRESCRA]  NUMERIC (18)                                                                     NULL,
    [MEDICACUSTODIA] BIT                                                                              NULL,
    [IDAUTORIZACION] NUMERIC (18)                                                                     NULL,
    [IDHCORDQUIMIO]  INT                                                                              NULL,
    [IDHCORDCICLOSD] INT                                                                              NULL,
    [IDCITA]         INT                                                                              NULL,
    [FORMAPRESCRIBE] TINYINT                                                                          NULL,
    [ITEMDOSIS]      TINYINT                                                                          NULL,
    CONSTRAINT [PK_HCENF] PRIMARY KEY CLUSTERED ([CONSECUTI] ASC),
    CONSTRAINT [FK_HCHOJAMED_ADINGRESO] FOREIGN KEY ([NUMINGRES]) REFERENCES [dbo].[ADINGRESO] ([NUMINGRES]),
    CONSTRAINT [FK_HCHOJAMED_HCORDQUIMIO] FOREIGN KEY ([IDHCORDQUIMIO]) REFERENCES [EHR].[HCORDQUIMIO] ([ID]),
    CONSTRAINT [FK_HCHOJAMED_HCQUICICLOS] FOREIGN KEY ([IDHCORDCICLOSD]) REFERENCES [EHR].[HCORDCICLOSD] ([ID]),
    CONSTRAINT [FK_HCHOJAMED_INPACIENT] FOREIGN KEY ([IPCODPACI]) REFERENCES [dbo].[INPACIENT] ([IPCODPACI])
);


GO
ADD SENSITIVITY CLASSIFICATION TO
    [dbo].[HCHOJAMED].[IPCODPACI]
    WITH (LABEL = 'Confidential - PII', INFORMATION_TYPE = 'National ID');


GO
ADD SENSITIVITY CLASSIFICATION TO
    [dbo].[HCHOJAMED].[CODPROSAL]
    WITH (LABEL = 'Confidential - PII', INFORMATION_TYPE = 'National ID');


GO
CREATE NONCLUSTERED INDEX [IX_Calendario]
    ON [dbo].[HCHOJAMED]([IPCODPACI] ASC, [NUMINGRES] ASC, [CABESTADO] ASC, [FECPROAPL] ASC)
    INCLUDE([CODPRODUC], [CODPROSAL], [CODUNIMED], [CODVIAADM], [CONSECUTI], [DOSISPROD], [DURACIDOS], [FECAPLMED], [FECINITRA], [FRECUENCI], [INDAPLMED], [MEDESTADO], [UNIDURFIJ], [UNIFRECUE], [VALDURFIJ]);


GO
CREATE NONCLUSTERED INDEX [IX_AplicacionesPendientes]
    ON [dbo].[HCHOJAMED]([CODCENATE] ASC, [UFUCODIGO] ASC, [MEDESTADO] ASC, [CABESTADO] ASC, [FECPROAPL] ASC)
    INCLUDE([CODPRODUC], [CODUNIMED], [CONSECUTI], [DOSISPROD], [DURACIDOS], [IPCODPACI], [NUMINGRES]);


GO
ALTER INDEX [IX_AplicacionesPendientes]
    ON [dbo].[HCHOJAMED] DISABLE;




GO
CREATE NONCLUSTERED INDEX [IX_HCHOJAMED_CODPRODUC_IDHCORDCICLOSD_MEDESTADO_NUMINGRES]
    ON [dbo].[HCHOJAMED]([CODPRODUC] ASC, [IDHCORDCICLOSD] ASC)
    INCLUDE([MEDESTADO], [NUMINGRES]);


GO
ALTER INDEX [IX_HCHOJAMED_CODPRODUC_IDHCORDCICLOSD_MEDESTADO_NUMINGRES]
    ON [dbo].[HCHOJAMED] DISABLE;




GO
CREATE NONCLUSTERED INDEX [IX_HCHOJAMED_IDHCORDQUIMIO_MEDESTADO]
    ON [dbo].[HCHOJAMED]([IDHCORDQUIMIO] ASC)
    INCLUDE([MEDESTADO]);


GO
CREATE NONCLUSTERED INDEX [IX_Medicamentos]
    ON [dbo].[HCHOJAMED]([IPCODPACI] ASC, [NUMINGRES] ASC, [MEDESTADO] ASC, [FECAPLMED] ASC)
    INCLUDE([CANDESCON], [CODPROAPL], [CODPRODUC], [CODTIPEST], [CODUNIMED], [CODVIAADM], [DOSISPROD], [DURACIDOS], [FRECUENCI], [UFUCODIGO], [UNIFRECUE]);


GO
CREATE NONCLUSTERED INDEX [XL_perfilFarmacoterapeutico]
    ON [dbo].[HCHOJAMED]([NUMINGRES] ASC, [CODPRODUC] ASC, [FECAPLMED] ASC);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador secuencial del item de dosis personalizada dentro de un tratamiento medicamentoso (INT, tipo numérico)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHOJAMED', @level2type = N'COLUMN', @level2name = N'ITEMDOSIS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Indica el item de la dosis personalizada', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHOJAMED', @level2type = N'COLUMN', @level2name = N'ITEMDOSIS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHOJAMED', @level2type = N'COLUMN', @level2name = N'ITEMDOSIS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Modalidad de prescripción: 0=Estándar (protocolo), 1=Personalizada (ajustada al paciente). Indica cómo se prescribió el medicamento (TINYINT)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHOJAMED', @level2type = N'COLUMN', @level2name = N'FORMAPRESCRIBE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Forma de prescripción: 0:Estandar, 1:Personalizada', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHOJAMED', @level2type = N'COLUMN', @level2name = N'FORMAPRESCRIBE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHOJAMED', @level2type = N'COLUMN', @level2name = N'FORMAPRESCRIBE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único de la cita de quimioterapia o oncología asociada a este registro medicamentoso (FK a AGASICITA)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHOJAMED', @level2type = N'COLUMN', @level2name = N'IDCITA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'ID de la cita de Quimioterapia', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHOJAMED', @level2type = N'COLUMN', @level2name = N'IDCITA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHOJAMED', @level2type = N'COLUMN', @level2name = N'IDCITA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del ciclo de quimioterapia al cual pertenece este medicamento; rastrea ciclos de tratamiento oncológico (FK a HCORDCICLOSD)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHOJAMED', @level2type = N'COLUMN', @level2name = N'IDHCORDCICLOSD';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'ID del ciclo de quimioterapia', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHOJAMED', @level2type = N'COLUMN', @level2name = N'IDHCORDCICLOSD';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHOJAMED', @level2type = N'COLUMN', @level2name = N'IDHCORDCICLOSD';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del esquema o protocolo de quimioterapia base para este medicamento; vincula a protocolos oncológicos (FK a HCORDQUIMIO)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHOJAMED', @level2type = N'COLUMN', @level2name = N'IDHCORDQUIMIO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'ID del esquema de quimioterapia', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHOJAMED', @level2type = N'COLUMN', @level2name = N'IDHCORDQUIMIO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHOJAMED', @level2type = N'COLUMN', @level2name = N'IDHCORDQUIMIO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Referencia a la autorización de servicio médico (FK a ADAUTSERC); vincula con autorizaciones de salud/cobertura', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHOJAMED', @level2type = N'COLUMN', @level2name = N'IDAUTORIZACION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Relación con la tabla ADAUTSERC ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHOJAMED', @level2type = N'COLUMN', @level2name = N'IDAUTORIZACION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHOJAMED', @level2type = N'COLUMN', @level2name = N'IDAUTORIZACION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador booleano: si el medicamento es custodia del paciente, es decir, suministrado por el paciente mismo (no por institución)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHOJAMED', @level2type = N'COLUMN', @level2name = N'MEDICACUSTODIA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Campo que me identifica si el medicamento es de custodia, es decir si el medicamento es suministrado ya por el paciente.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHOJAMED', @level2type = N'COLUMN', @level2name = N'MEDICACUSTODIA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHOJAMED', @level2type = N'COLUMN', @level2name = N'MEDICACUSTODIA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Consecutivo único de la prescripción médica generadora de este medicamento; enlace con historial de prescripciones (NUMERIC)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHOJAMED', @level2type = N'COLUMN', @level2name = N'CONSECPRESCRA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'consecutivo de prescripcion medica', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHOJAMED', @level2type = N'COLUMN', @level2name = N'CONSECPRESCRA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHOJAMED', @level2type = N'COLUMN', @level2name = N'CONSECPRESCRA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código clasificador del tipo de estancia/contexto clínico (internación, ambulatorio, urgencia, etc.); CHAR(3)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHOJAMED', @level2type = N'COLUMN', @level2name = N'CODTIPEST';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo del tipo de estancia', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHOJAMED', @level2type = N'COLUMN', @level2name = N'CODTIPEST';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHOJAMED', @level2type = N'COLUMN', @level2name = N'CODTIPEST';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Descripción detallada de administración manual del medicamento: instrucciones, notas clínicas de aplicación (VARCHAR MAX)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHOJAMED', @level2type = N'COLUMN', @level2name = N'DESADMINI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Administracion del medicamento (Dosis Manual)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHOJAMED', @level2type = N'COLUMN', @level2name = N'DESADMINI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHOJAMED', @level2type = N'COLUMN', @level2name = N'DESADMINI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Bandera booleano: 1=Formulación manual/personalizada por profesional, 0=Automática/protocolo estándar', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHOJAMED', @level2type = N'COLUMN', @level2name = N'FORMUMANU';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Formulacion Manual', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHOJAMED', @level2type = N'COLUMN', @level2name = N'FORMUMANU';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHOJAMED', @level2type = N'COLUMN', @level2name = N'FORMUMANU';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Cantidad de dosis a descontar/restar del inventario de medicamentos disponibles (INT)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHOJAMED', @level2type = N'COLUMN', @level2name = N'CANDESCON';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Cantidad a Descontar', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHOJAMED', @level2type = N'COLUMN', @level2name = N'CANDESCON';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHOJAMED', @level2type = N'COLUMN', @level2name = N'CANDESCON';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código de identificación del usuario/profesional que suspendió o modificó el medicamento (CHAR 20, PII sensible)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHOJAMED', @level2type = N'COLUMN', @level2name = N'CODUSUSUS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo del usuario que suspende', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHOJAMED', @level2type = N'COLUMN', @level2name = N'CODUSUSUS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHOJAMED', @level2type = N'COLUMN', @level2name = N'CODUSUSUS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Motivo textual de la suspensión del medicamento: contraindicación, reacción adversa, mejoría clínica, etc. (VARCHAR 2000)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHOJAMED', @level2type = N'COLUMN', @level2name = N'MOTSUSMED';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Motivo de Suspension', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHOJAMED', @level2type = N'COLUMN', @level2name = N'MOTSUSMED';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHOJAMED', @level2type = N'COLUMN', @level2name = N'MOTSUSMED';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicaciones clínicas detalladas para la aplicación del medicamento: síntomas, controles, observaciones (VARCHAR MAX)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHOJAMED', @level2type = N'COLUMN', @level2name = N'INDAPLMED';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Indicaciones de Aplicacion de Medicamentos', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHOJAMED', @level2type = N'COLUMN', @level2name = N'INDAPLMED';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHOJAMED', @level2type = N'COLUMN', @level2name = N'INDAPLMED';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Estado del medicamento en cabecera: 1=Activo, 2=Terminado, 3=Suspendido por modificación. Refleja ciclo de vida (CHAR 1)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHOJAMED', @level2type = N'COLUMN', @level2name = N'CABESTADO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Estado del Medicamento  1: Activo  2: Terminado  3: Suspendido por modficacion del medicamento', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHOJAMED', @level2type = N'COLUMN', @level2name = N'CABESTADO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHOJAMED', @level2type = N'COLUMN', @level2name = N'CABESTADO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Estado operativo del medicamento: 1=Pendiente de aplicar, 2=Aplicado, 3=Suspendido. Indica etapa de ejecución (CHAR 1)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHOJAMED', @level2type = N'COLUMN', @level2name = N'MEDESTADO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Estado del Medicamento  1: Pendiente  2: Aplicado  3: Suspendido', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHOJAMED', @level2type = N'COLUMN', @level2name = N'MEDESTADO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHOJAMED', @level2type = N'COLUMN', @level2name = N'MEDESTADO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora de conclusión/cierre de la última dosis del tratamiento medicamentoso (DATETIME nullable)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHOJAMED', @level2type = N'COLUMN', @level2name = N'FECFINDOS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha Final de la Dosis', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHOJAMED', @level2type = N'COLUMN', @level2name = N'FECFINDOS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHOJAMED', @level2type = N'COLUMN', @level2name = N'FECFINDOS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Unidad temporal de la duración fija: 1=Minutos, 2=Horas, 3=Días. Cualificador de VALDURFIJ (CHAR 1)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHOJAMED', @level2type = N'COLUMN', @level2name = N'UNIDURFIJ';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Unidad del Valor de la duracion fija:  1: Minutos  2: Horas  3: Dias', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHOJAMED', @level2type = N'COLUMN', @level2name = N'UNIDURFIJ';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHOJAMED', @level2type = N'COLUMN', @level2name = N'UNIDURFIJ';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Valor numérico entero de la duración fija del tratamiento según UNIDURFIJ (INT nullable)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHOJAMED', @level2type = N'COLUMN', @level2name = N'VALDURFIJ';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Valor de la duracion fija', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHOJAMED', @level2type = N'COLUMN', @level2name = N'VALDURFIJ';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHOJAMED', @level2type = N'COLUMN', @level2name = N'VALDURFIJ';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Duración total del esquema de dosis: expresión textual (ej: ''''10 dias'''', ''''2 semanas''''); CHAR(20)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHOJAMED', @level2type = N'COLUMN', @level2name = N'DURACIDOS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Duracion de la Dosis', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHOJAMED', @level2type = N'COLUMN', @level2name = N'DURACIDOS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHOJAMED', @level2type = N'COLUMN', @level2name = N'DURACIDOS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Unidad temporal de la frecuencia: 1=Minutos, 2=Horas, 3=Días. Cualificador de FRECUENCI (CHAR 1)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHOJAMED', @level2type = N'COLUMN', @level2name = N'UNIFRECUE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Unidad del Valor de la Frecuencia:  1: Minutos  2: Horas  3: Dias', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHOJAMED', @level2type = N'COLUMN', @level2name = N'UNIFRECUE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHOJAMED', @level2type = N'COLUMN', @level2name = N'UNIFRECUE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Frecuencia numérica de administración: cada cuántas unidades se aplica el medicamento (INT nullable)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHOJAMED', @level2type = N'COLUMN', @level2name = N'FRECUENCI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Frecuencia', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHOJAMED', @level2type = N'COLUMN', @level2name = N'FRECUENCI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHOJAMED', @level2type = N'COLUMN', @level2name = N'FRECUENCI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código de la unidad de medida farmacéutica: mL, mg, UI, comprimidos, ampolla, etc. (CHAR 30)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHOJAMED', @level2type = N'COLUMN', @level2name = N'CODUNIMED';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo de la Unidad de Medida', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHOJAMED', @level2type = N'COLUMN', @level2name = N'CODUNIMED';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHOJAMED', @level2type = N'COLUMN', @level2name = N'CODUNIMED';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Dosis/cantidad cuantitativa del medicamento por aplicación (NUMERIC 18,2; puede ser fraccionada)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHOJAMED', @level2type = N'COLUMN', @level2name = N'DOSISPROD';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Dosis', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHOJAMED', @level2type = N'COLUMN', @level2name = N'DOSISPROD';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHOJAMED', @level2type = N'COLUMN', @level2name = N'DOSISPROD';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código de la vía de administración: VO (oral), IV (intravenosa), IM (intramuscular), SC (subcutánea), etc. (CHAR 20)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHOJAMED', @level2type = N'COLUMN', @level2name = N'CODVIAADM';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo de la Via de Administracion Comun', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHOJAMED', @level2type = N'COLUMN', @level2name = N'CODVIAADM';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHOJAMED', @level2type = N'COLUMN', @level2name = N'CODVIAADM';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora del servidor al registrar este medicamento en sistema; timestamp de auditoría (DATETIME, default getdate())', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHOJAMED', @level2type = N'COLUMN', @level2name = N'FECREGSIS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha/Hora del servidor para el registro', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHOJAMED', @level2type = N'COLUMN', @level2name = N'FECREGSIS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHOJAMED', @level2type = N'COLUMN', @level2name = N'FECREGSIS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora real/efectiva de la aplicación del medicamento al paciente; registra ejecución real (DATETIME nullable)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHOJAMED', @level2type = N'COLUMN', @level2name = N'FECAPLMED';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha/Hora real de aplicacion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHOJAMED', @level2type = N'COLUMN', @level2name = N'FECAPLMED';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHOJAMED', @level2type = N'COLUMN', @level2name = N'FECAPLMED';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora programada/ordenada para la aplicación del medicamento según prescripción (DATETIME)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHOJAMED', @level2type = N'COLUMN', @level2name = N'FECPROAPL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha/Hora Programada de aplicacion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHOJAMED', @level2type = N'COLUMN', @level2name = N'FECPROAPL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHOJAMED', @level2type = N'COLUMN', @level2name = N'FECPROAPL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha de inicio del tratamiento medicamentoso; marca el primer día de terapia (DATETIME)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHOJAMED', @level2type = N'COLUMN', @level2name = N'FECINITRA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha incial de Tratamiento', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHOJAMED', @level2type = N'COLUMN', @level2name = N'FECINITRA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHOJAMED', @level2type = N'COLUMN', @level2name = N'FECINITRA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del producto medicamentoso/fármaco: identifica el medicamento específico prescrito (CHAR 20)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHOJAMED', @level2type = N'COLUMN', @level2name = N'CODPRODUC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo del Producto', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHOJAMED', @level2type = N'COLUMN', @level2name = N'CODPRODUC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHOJAMED', @level2type = N'COLUMN', @level2name = N'CODPRODUC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del profesional de la salud (médico, enfermero) que aplicó o suspendió el medicamento (CHAR 20, PII sensible)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHOJAMED', @level2type = N'COLUMN', @level2name = N'CODPROAPL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo del Profesional que Aplica / Suspende', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHOJAMED', @level2type = N'COLUMN', @level2name = N'CODPROAPL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHOJAMED', @level2type = N'COLUMN', @level2name = N'CODPROAPL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del profesional de la salud que prescribió/ordenó el medicamento; médico tratante (CHAR 20, PII sensible/Identification_Ofuscado)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHOJAMED', @level2type = N'COLUMN', @level2name = N'CODPROSAL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo del Profesional de la Salud que prescribe', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHOJAMED', @level2type = N'COLUMN', @level2name = N'CODPROSAL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHOJAMED', @level2type = N'COLUMN', @level2name = N'CODPROSAL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código de la unidad funcional donde se prescribió/aplicó el medicamento: farmacia, unidad de cuidados, oncología, etc. (CHAR 10)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHOJAMED', @level2type = N'COLUMN', @level2name = N'UFUCODIGO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo de la Unidad Funcional', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHOJAMED', @level2type = N'COLUMN', @level2name = N'UFUCODIGO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHOJAMED', @level2type = N'COLUMN', @level2name = N'UFUCODIGO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del centro de atención/institución donde se registra la medicación: hospital, clínica, consultorio (CHAR 10)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHOJAMED', @level2type = N'COLUMN', @level2name = N'CODCENATE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo del Centro de Atencion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHOJAMED', @level2type = N'COLUMN', @level2name = N'CODCENATE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHOJAMED', @level2type = N'COLUMN', @level2name = N'CODCENATE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número único del ingreso/episodio del paciente; vincula medicamento a internación específica (CHAR 10, FK a ADINGRESO)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHOJAMED', @level2type = N'COLUMN', @level2name = N'NUMINGRES';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Numero del Ingreso', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHOJAMED', @level2type = N'COLUMN', @level2name = N'NUMINGRES';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHOJAMED', @level2type = N'COLUMN', @level2name = N'NUMINGRES';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código único del paciente; equivalente a cédula/documento de identidad (VARCHAR 25, PII sensible/Identification_Ofuscado, FK a INPACIENT)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHOJAMED', @level2type = N'COLUMN', @level2name = N'IPCODPACI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo del Paciente', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHOJAMED', @level2type = N'COLUMN', @level2name = N'IPCODPACI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHOJAMED', @level2type = N'COLUMN', @level2name = N'IPCODPACI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Consecutivo autonumérico secuencial; identificador único primario de este medicamento en historia clínica (NUMERIC 18, IDENTITY)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHOJAMED', @level2type = N'COLUMN', @level2name = N'CONSECUTI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Consecutiov Autonumerico', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHOJAMED', @level2type = N'COLUMN', @level2name = N'CONSECUTI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHOJAMED', @level2type = N'COLUMN', @level2name = N'CONSECUTI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Hoja de medicación del paciente en historia clínica. Registra cada medicamento ordenado o administrado durante un ingreso hospitalario, incluyendo dosis, frecuencia, vía de administración, fechas de inicio y fin, estado de la orden y quién la prescribió o aplicó.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHOJAMED';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHOJAMED';
