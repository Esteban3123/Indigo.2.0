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
ALTER TABLE [dbo].[HCPLAOTRPROCUPS] NOCHECK CONSTRAINT [FK_HCPLAOTRPROCUPS_CUPSEntityContractDescriptions];


GO
ALTER TABLE [dbo].[HCPLAOTRPROCUPS] NOCHECK CONSTRAINT [FK_HCPLAOTRPROCUPS_INCUPSIPS];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Guarda el Id/Auto de la tabla origen del registro:


- ID / AMBORDOTROSPRO
- AUTO / HCORDPROQ
- AUTO / HCORDPRON', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPLAOTRPROCUPS', @level2type = N'COLUMN', @level2name = N'IdSourceTable';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Guarda el nombre de la tabla origen al que pertenece el registro:

- AMBORDOTROSPRO
- HCORDPROQ
- HCORDPRON', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPLAOTRPROCUPS', @level2type = N'COLUMN', @level2name = N'SourceTable';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Campo que almacena el Id de la unidad funcional asociada al ''área de otros procedimientos'' seleccionada desde el dashboard otros procedimientos', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPLAOTRPROCUPS', @level2type = N'COLUMN', @level2name = N'IdUnitFunctionalEnd';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Campo donde se almacena el Id del campo ''Areas otros procedimientos'' seleccionada desde el dashboard otros procedimientos', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPLAOTRPROCUPS', @level2type = N'COLUMN', @level2name = N'IdAreasOtherProcedures';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Generar orden de servicio', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPLAOTRPROCUPS', @level2type = N'COLUMN', @level2name = N'GENSERVICEORDER';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Campo que guarda el id de la descripcion relacionada (id tabla Contract.CUPSEntityContractDescriptions)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPLAOTRPROCUPS', @level2type = N'COLUMN', @level2name = N'IDDESCRIPCIONRELACIONADA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Este campo me indica si se debe facturar o no el CUPS, es decir si se debe o no listar en control cuentas.

En false llega cuando el servicio ya se a facturado desde control de servicios ambulatirios para no cobrar doblemente el CUPS

Pero COMO EL medico puede agregar mas CUPS en la rejilla, estos cups se deben facturar y son los que deben llegar en en TRUE para poder facturarlos.

VIE ERP lee esta tabla para listar en control cuentas.  




', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPLAOTRPROCUPS', @level2type = N'COLUMN', @level2name = N'FACTURAR';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Descripcion de las plantillas diligenciadas', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPLAOTRPROCUPS', @level2type = N'COLUMN', @level2name = N'PLANTILLA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Cantidad', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPLAOTRPROCUPS', @level2type = N'COLUMN', @level2name = N'CANTIDAD';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo del procedimiento', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPLAOTRPROCUPS', @level2type = N'COLUMN', @level2name = N'CODSERIPS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id cabecera tabla HCPLAOTRPROC', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPLAOTRPROCUPS', @level2type = N'COLUMN', @level2name = N'IDHCPLAOTRPROC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id autonumerico detalle de Procedimientos', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPLAOTRPROCUPS', @level2type = N'COLUMN', @level2name = N'ID';

