CREATE TABLE [dbo].[HCPLAOTRPROCUPS] (
    [ID]                       INT           IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [IDHCPLAOTRPROC]           INT           NOT NULL,
    [CODSERIPS]                CHAR (20)     NOT NULL,
    [CANTIDAD]                 INT           NOT NULL,
    [PLANTILLA]                VARCHAR (MAX) NULL,
    [FACTURAR]                 BIT           NOT NULL,
    [IDDESCRIPCIONRELACIONADA] INT           NULL,
    [GENSERVICEORDER]          INT           NULL,
    [IdAreasOtherProcedures]   INT           NULL,
    [IdUnitFunctionalEnd]      CHAR (10)     NULL,
    [SourceTable]              VARCHAR (25)  NULL,
    [IdSourceTable]            INT           NULL,
    CONSTRAINT [PK_HCPLAOTRPROCUPS] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_HCPLAOTRPROCUPS_CUPSEntityContractDescriptions] FOREIGN KEY ([IDDESCRIPCIONRELACIONADA]) REFERENCES [Contract].[CUPSEntityContractDescriptions] ([Id]),
    CONSTRAINT [FK_HCPLAOTRPROCUPS_HCAREASC] FOREIGN KEY ([IdAreasOtherProcedures]) REFERENCES [dbo].[HCAREASC] ([ID]),
    CONSTRAINT [FK_HCPLAOTRPROCUPS_INCUPSIPS] FOREIGN KEY ([CODSERIPS]) REFERENCES [dbo].[INCUPSIPS] ([CODSERIPS]),
    CONSTRAINT [FK_HCPLAOTRPROCUPS_UFUCODIGO] FOREIGN KEY ([IdUnitFunctionalEnd]) REFERENCES [dbo].[INUNIFUNC] ([UFUCODIGO])
);








GO



GO



GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador auto de la tabla origen del registro (ID de AMBORDOTROSPRO, AUTO de HCORDPROQ o HCORDPRON). Referencia a procedimiento ambulatorio, orden de procedimiento quirúrgico u ordinario origen.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPLAOTRPROCUPS', @level2type = N'COLUMN', @level2name = N'IdSourceTable';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Guarda el Id/Auto de la tabla origen del registro:


- ID / AMBORDOTROSPRO
- AUTO / HCORDPROQ
- AUTO / HCORDPRON', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPLAOTRPROCUPS', @level2type = N'COLUMN', @level2name = N'IdSourceTable';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPLAOTRPROCUPS', @level2type = N'COLUMN', @level2name = N'IdSourceTable';








GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Nombre de tabla origen del procedimiento (AMBORDOTROSPRO, HCORDPROQ, HCORDPRON). Indica si proviene de orden ambulatoria, quirúrgica u ordinaria. Trazabilidad de fuente.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPLAOTRPROCUPS', @level2type = N'COLUMN', @level2name = N'SourceTable';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Guarda el nombre de la tabla origen al que pertenece el registro:

- AMBORDOTROSPRO
- HCORDPROQ
- HCORDPRON', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPLAOTRPROCUPS', @level2type = N'COLUMN', @level2name = N'SourceTable';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPLAOTRPROCUPS', @level2type = N'COLUMN', @level2name = N'SourceTable';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de unidad funcional final donde se ejecuta el procedimiento. Enlace a INUNIFUNC. Seleccionado desde dashboard otros procedimientos.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPLAOTRPROCUPS', @level2type = N'COLUMN', @level2name = N'IdUnitFunctionalEnd';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Campo que almacena el Id de la unidad funcional asociada al ''''área de otros procedimientos'''' seleccionada desde el dashboard otros procedimientos', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPLAOTRPROCUPS', @level2type = N'COLUMN', @level2name = N'IdUnitFunctionalEnd';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPLAOTRPROCUPS', @level2type = N'COLUMN', @level2name = N'IdUnitFunctionalEnd';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del área de otros procedimientos seleccionada. Referencia FK a HCAREASC. Clasifica procedimiento por área administrativa/clínica.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPLAOTRPROCUPS', @level2type = N'COLUMN', @level2name = N'IdAreasOtherProcedures';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Campo donde se almacena el Id del campo ''''Areas otros procedimientos'''' seleccionada desde el dashboard otros procedimientos', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPLAOTRPROCUPS', @level2type = N'COLUMN', @level2name = N'IdAreasOtherProcedures';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPLAOTRPROCUPS', @level2type = N'COLUMN', @level2name = N'IdAreasOtherProcedures';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador de generación de orden de servicio. Disparo de creación de orden para seguimiento y control de ejecución del procedimiento.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPLAOTRPROCUPS', @level2type = N'COLUMN', @level2name = N'GENSERVICEORDER';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Generar orden de servicio', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPLAOTRPROCUPS', @level2type = N'COLUMN', @level2name = N'GENSERVICEORDER';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPLAOTRPROCUPS', @level2type = N'COLUMN', @level2name = N'GENSERVICEORDER';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de descripción contractual CUPS enlazada (FK Contract.CUPSEntityContractDescriptions). Detalle del procedimiento según contrato vigente.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPLAOTRPROCUPS', @level2type = N'COLUMN', @level2name = N'IDDESCRIPCIONRELACIONADA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Campo que guarda el id de la descripcion relacionada (id tabla Contract.CUPSEntityContractDescriptions)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPLAOTRPROCUPS', @level2type = N'COLUMN', @level2name = N'IDDESCRIPCIONRELACIONADA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPLAOTRPROCUPS', @level2type = N'COLUMN', @level2name = N'IDDESCRIPCIONRELACIONADA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Booleano de facturación del CUPS. TRUE si debe incluirse en control de cuentas. FALSE si ya facturado desde control servicios ambulatorios. Evita doble cobro. CUPS agregados por médico en rejilla deben llegar TRUE.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPLAOTRPROCUPS', @level2type = N'COLUMN', @level2name = N'FACTURAR';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Este campo me indica si se debe facturar o no el CUPS, es decir si se debe o no listar en control cuentas.

En false llega cuando el servicio ya se a facturado desde control de servicios ambulatirios para no cobrar doblemente el CUPS

Pero COMO EL medico puede agregar mas CUPS en la rejilla, estos cups se deben facturar y son los que deben llegar en en TRUE para poder facturarlos.

VIE ERP lee esta tabla para listar en control cuentas.  




', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPLAOTRPROCUPS', @level2type = N'COLUMN', @level2name = N'FACTURAR';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPLAOTRPROCUPS', @level2type = N'COLUMN', @level2name = N'FACTURAR';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Contenido de plantilla clínica diligenciada asociada al procedimiento. VARCHAR(MAX). Almacena datos variables, protocolos completados.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPLAOTRPROCUPS', @level2type = N'COLUMN', @level2name = N'PLANTILLA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Descripcion de las plantillas diligenciadas', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPLAOTRPROCUPS', @level2type = N'COLUMN', @level2name = N'PLANTILLA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPLAOTRPROCUPS', @level2type = N'COLUMN', @level2name = N'PLANTILLA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Cantidad de unidades del CUPS/procedimiento facturado o a facturar. Entero positivo. Base para cálculo de valor total.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPLAOTRPROCUPS', @level2type = N'COLUMN', @level2name = N'CANTIDAD';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Cantidad', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPLAOTRPROCUPS', @level2type = N'COLUMN', @level2name = N'CANTIDAD';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPLAOTRPROCUPS', @level2type = N'COLUMN', @level2name = N'CANTIDAD';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código CUPS/RIPS del procedimiento. Identificador estándar del servicio de salud facturado. Enlace FK a INCUPSIPS.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPLAOTRPROCUPS', @level2type = N'COLUMN', @level2name = N'CODSERIPS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo del procedimiento', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPLAOTRPROCUPS', @level2type = N'COLUMN', @level2name = N'CODSERIPS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPLAOTRPROCUPS', @level2type = N'COLUMN', @level2name = N'CODSERIPS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de cabecera de la tabla HCPLAOTRPROC. Agrupa detalle de procedimientos bajo un evento de otros procedimientos.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPLAOTRPROCUPS', @level2type = N'COLUMN', @level2name = N'IDHCPLAOTRPROC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id cabecera tabla HCPLAOTRPROC', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPLAOTRPROCUPS', @level2type = N'COLUMN', @level2name = N'IDHCPLAOTRPROC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPLAOTRPROCUPS', @level2type = N'COLUMN', @level2name = N'IDHCPLAOTRPROC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador autonumérico de detalle. Clave primaria clustered. Cada fila = una línea de procedimiento en facturación.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPLAOTRPROCUPS', @level2type = N'COLUMN', @level2name = N'ID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id autonumerico detalle de Procedimientos', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPLAOTRPROCUPS', @level2type = N'COLUMN', @level2name = N'ID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPLAOTRPROCUPS', @level2type = N'COLUMN', @level2name = N'ID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Códigos de servicio CUPS asociados a otros procedimientos registrados en plantillas clínicas. Detalla qué servicios o procedimientos adicionales se vinculan a una entrada de procedimientos en la historia clínica, indicando cantidades, si deben facturarse y el origen del registro.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPLAOTRPROCUPS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPLAOTRPROCUPS';
