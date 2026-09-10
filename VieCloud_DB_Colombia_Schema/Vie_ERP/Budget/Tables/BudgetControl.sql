CREATE TABLE [Budget].[BudgetControl] (
    [Id]             INT           IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [DocumentNumber] VARCHAR (100) NOT NULL,
    [DocumentType]   INT           NOT NULL,
    [DocumentUser]   VARCHAR (50)  NOT NULL,
    [DocumentDate]   DATETIME      NOT NULL,
    CONSTRAINT [PK_BudgetControl__Id] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [UQ_BudgetControl__DocumentNumber__DocumentType] UNIQUE NONCLUSTERED ([DocumentNumber] ASC, [DocumentType] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha del documento, timestamp de creación o modificación del registro presupuestal (DATETIME)', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'BudgetControl', @level2type = N'COLUMN', @level2name = N'DocumentDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha del Documento', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'BudgetControl', @level2type = N'COLUMN', @level2name = N'DocumentDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'BudgetControl', @level2type = N'COLUMN', @level2name = N'DocumentDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Usuario que creó el documento, identificación del profesional o administrador responsable (VARCHAR 50, auditoria)', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'BudgetControl', @level2type = N'COLUMN', @level2name = N'DocumentUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Usuario que creo el documento', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'BudgetControl', @level2type = N'COLUMN', @level2name = N'DocumentUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'BudgetControl', @level2type = N'COLUMN', @level2name = N'DocumentUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tipo de documento presupuestal: modificación/traslado PTO/PAC, reconocimiento, recaudo, disponibilidad, compromiso, obligación, orden de pago, liberación, reintegro, reserva, cuenta por pagar/cobrar, VFT, suspensión/levantamiento, prórroga, gastos (INT 1-34)', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'BudgetControl', @level2type = N'COLUMN', @level2name = N'DocumentType';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Tipo del documento   (MODIFICACION AL PTO = 1,  TRASLADO AL PTO = 2  ,MODIFICACION AL PAC =3,  TRASLADO AL PAC = 4  ,RECONOCIMIENTO = 5,  MODIFICACION AL RECONOCIMIENTO = 6,  RECAUDO = 7,  MODIFICACION AL RECAUDO = 8,  DISPONIBILIDAD = 9,  MODIFICACION A LA DISPONIBILIDAD = 10,  COMPROMISO = 11,  MODIFICACION AL COMPROMISO = 12,  PRORROGA DE DISPONIBILIDADES = 13,  OBLIGACION = 14,  MODIFICACION A LA OBLIGACION = 15,  ORDEN DE PAGO = 16,  MODIFICACION A LA ORDEN DE PAGO = 17,  LIBERACION DE RECURSOS = 18,  REINTEGRO = 19,  RESERVA = 21,  CUENTA POR PAGAR =22,  DISPONIBILIDAD DE VFT = 23,  COMPROMISO DE VFT = 24,  OBLIGACION DE VFT = 25,  ORDEN DE PAGO DE VFT = 26,  SUSPENCION PRESUPUESTAL = 27,  LEVANTAMIENTO PRESUPUESTAL = 28,  CUENTAS POR COBRAR = 29,  PRORROGA DE DOCUMENTOS = 30,  MODIFICACION AL PTO GASTOS = 31,  TRASLADO AL PTO GASTOS = 32  ,MODIFICACION AL PAC GASTOS = 33,  TRASLADO AL PAC GASTOS = 34)', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'BudgetControl', @level2type = N'COLUMN', @level2name = N'DocumentType';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'BudgetControl', @level2type = N'COLUMN', @level2name = N'DocumentType';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Consecutivo único del documento, número secuencial de identificación presupuestal (VARCHAR 100, parte de clave única con DocumentType)', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'BudgetControl', @level2type = N'COLUMN', @level2name = N'DocumentNumber';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'consecutivo del documento', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'BudgetControl', @level2type = N'COLUMN', @level2name = N'DocumentNumber';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'BudgetControl', @level2type = N'COLUMN', @level2name = N'DocumentNumber';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único autonumérico de la tabla BudgetControl, primary key (INT IDENTITY)', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'BudgetControl', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'ID Autonumerico de la tabla', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'BudgetControl', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'BudgetControl', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Registro de control presupuestario: guarda los documentos de movimiento o aprobación de presupuesto, indicando el número de documento, el tipo de operación, el usuario responsable y la fecha en que se generó cada transacción presupuestal.', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'BudgetControl';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'BudgetControl';
