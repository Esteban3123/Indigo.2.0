CREATE TABLE [Portfolio].[PortfolioReclassification] (
    [Id]                  INT             IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [Code]                VARCHAR (20)    NOT NULL,
    [DocumentType]        TINYINT         NOT NULL,
    [AccountReceivableId] INT             NOT NULL,
    [SourceAccountId]     INT             NOT NULL,
    [TargetAccountId]     INT             NOT NULL,
    [Value]               NUMERIC (18, 2) NOT NULL,
    [Status]              TINYINT         NOT NULL,
    [CreationUser]        VARCHAR (20)    NOT NULL,
    [CreationDate]        DATETIME        NOT NULL,
    [ModificationUser]    VARCHAR (20)    NULL,
    [ModificationDate]    DATETIME        NULL,
    [ConfirmationUser]    VARCHAR (20)    NULL,
    [ConfirmationDate]    DATETIME        NULL,
    [AnnulmentUser]       VARCHAR (20)    NULL,
    [AnnulmentDate]       DATETIME        NULL,
    [TimeStamp]           ROWVERSION      NOT NULL,
    [DocumentDate]        DATETIME        NULL,
    [EntityId]            INT             NULL,
    [EntityCode]          VARCHAR (20)    NULL,
    [EntityName]          VARCHAR (250)   NULL,
    CONSTRAINT [PK_PortfolioReclassification] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_PortfolioReclassification_AccountReceivable] FOREIGN KEY ([AccountReceivableId]) REFERENCES [Portfolio].[AccountReceivable] ([Id]),
    CONSTRAINT [FK_PortfolioReclassification_MainAccounts] FOREIGN KEY ([SourceAccountId]) REFERENCES [GeneralLedger].[MainAccounts] ([Id]),
    CONSTRAINT [FK_PortfolioReclassification_MainAccounts1] FOREIGN KEY ([TargetAccountId]) REFERENCES [GeneralLedger].[MainAccounts] ([Id])
);




GO



GO



GO



GO
CREATE NONCLUSTERED INDEX [IX_Test]
    ON [Portfolio].[PortfolioReclassification]([AccountReceivableId] ASC, [TargetAccountId] ASC)
    INCLUDE([DocumentDate]);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Nombre completo de la entidad (institución, clínica, centro de salud) que origina o genera el documento de reclasificación. VARCHAR(250).', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'PortfolioReclassification', @level2type = N'COLUMN', @level2name = N'EntityName';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Nombre de la entidad quien genera el documento', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'PortfolioReclassification', @level2type = N'COLUMN', @level2name = N'EntityName';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'PortfolioReclassification', @level2type = N'COLUMN', @level2name = N'EntityName';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código identificador único de la entidad generadora del documento. VARCHAR(20). Búsqueda: institución, proveedor, centro.', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'PortfolioReclassification', @level2type = N'COLUMN', @level2name = N'EntityCode';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Código de la entidad quien genera el documento', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'PortfolioReclassification', @level2type = N'COLUMN', @level2name = N'EntityCode';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'PortfolioReclassification', @level2type = N'COLUMN', @level2name = N'EntityCode';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador numérico de la entidad que crea el documento. INT. Referencia a tabla de entidades/instituciones.', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'PortfolioReclassification', @level2type = N'COLUMN', @level2name = N'EntityId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de la entidad quien genera el documento', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'PortfolioReclassification', @level2type = N'COLUMN', @level2name = N'EntityId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'PortfolioReclassification', @level2type = N'COLUMN', @level2name = N'EntityId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha de emisión del documento de reclasificación. DATETIME. Búsqueda: cuándo se originó la reclasificación.', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'PortfolioReclassification', @level2type = N'COLUMN', @level2name = N'DocumentDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha del documento', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'PortfolioReclassification', @level2type = N'COLUMN', @level2name = N'DocumentDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'PortfolioReclassification', @level2type = N'COLUMN', @level2name = N'DocumentDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Marca temporal (TIMESTAMP) del evento: creación, modificación o registro del documento. Registra el instante exacto de cambios en el sistema.', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'PortfolioReclassification', @level2type = N'COLUMN', @level2name = N'TimeStamp';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Marca temporal en la que ocurrio determinado evento. Guarda el instante tiempo de la creacion , registro o modificacion de un archivo determinado.', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'PortfolioReclassification', @level2type = N'COLUMN', @level2name = N'TimeStamp';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'PortfolioReclassification', @level2type = N'COLUMN', @level2name = N'TimeStamp';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora en que se anuló o canceló la reclasificación. DATETIME NULL. Búsqueda: cuándo se revocó.', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'PortfolioReclassification', @level2type = N'COLUMN', @level2name = N'AnnulmentDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha Anulación', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'PortfolioReclassification', @level2type = N'COLUMN', @level2name = N'AnnulmentDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'PortfolioReclassification', @level2type = N'COLUMN', @level2name = N'AnnulmentDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Usuario/empleado que ejecutó la anulación. VARCHAR(20). Auditoría de quién revocó.', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'PortfolioReclassification', @level2type = N'COLUMN', @level2name = N'AnnulmentUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Usuario Anulación', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'PortfolioReclassification', @level2type = N'COLUMN', @level2name = N'AnnulmentUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'PortfolioReclassification', @level2type = N'COLUMN', @level2name = N'AnnulmentUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora en que se confirmó y validó la reclasificación. DATETIME NULL. Búsqueda: aprobación.', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'PortfolioReclassification', @level2type = N'COLUMN', @level2name = N'ConfirmationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha Confirmación', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'PortfolioReclassification', @level2type = N'COLUMN', @level2name = N'ConfirmationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'PortfolioReclassification', @level2type = N'COLUMN', @level2name = N'ConfirmationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Usuario/empleado que confirmó la reclasificación. VARCHAR(20). Auditoría de aprobador.', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'PortfolioReclassification', @level2type = N'COLUMN', @level2name = N'ConfirmationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Usuario Confirmación', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'PortfolioReclassification', @level2type = N'COLUMN', @level2name = N'ConfirmationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'PortfolioReclassification', @level2type = N'COLUMN', @level2name = N'ConfirmationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora de la última modificación del registro. DATETIME NULL. Búsqueda: cuándo cambió.', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'PortfolioReclassification', @level2type = N'COLUMN', @level2name = N'ModificationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha Modificación', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'PortfolioReclassification', @level2type = N'COLUMN', @level2name = N'ModificationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'PortfolioReclassification', @level2type = N'COLUMN', @level2name = N'ModificationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Usuario que realizó la última modificación. VARCHAR(20). Auditoría de cambios.', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'PortfolioReclassification', @level2type = N'COLUMN', @level2name = N'ModificationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Usuario Modificación', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'PortfolioReclassification', @level2type = N'COLUMN', @level2name = N'ModificationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'PortfolioReclassification', @level2type = N'COLUMN', @level2name = N'ModificationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora de creación inicial del registro de reclasificación. DATETIME. Búsqueda: cuándo se registró.', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'PortfolioReclassification', @level2type = N'COLUMN', @level2name = N'CreationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha de Creación', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'PortfolioReclassification', @level2type = N'COLUMN', @level2name = N'CreationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'PortfolioReclassification', @level2type = N'COLUMN', @level2name = N'CreationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Usuario que creó originalmente el documento. VARCHAR(20). Auditoría de origen.', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'PortfolioReclassification', @level2type = N'COLUMN', @level2name = N'CreationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Usuario Creación', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'PortfolioReclassification', @level2type = N'COLUMN', @level2name = N'CreationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'PortfolioReclassification', @level2type = N'COLUMN', @level2name = N'CreationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Estado de la reclasificación: 1=Registrado, 2=Confirmado, 3=Anulado. TINYINT. Búsqueda: estado, workflow.', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'PortfolioReclassification', @level2type = N'COLUMN', @level2name = N'Status';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Estado (registrado = 1,confirmado = 2,anulado = 3)', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'PortfolioReclassification', @level2type = N'COLUMN', @level2name = N'Status';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'PortfolioReclassification', @level2type = N'COLUMN', @level2name = N'Status';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Monto numérico a reclasificar (mover entre cuentas por cobrar). NUMERIC(18,2). En unidades monetarias.', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'PortfolioReclassification', @level2type = N'COLUMN', @level2name = N'Value';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Valor a reclasificar', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'PortfolioReclassification', @level2type = N'COLUMN', @level2name = N'Value';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'PortfolioReclassification', @level2type = N'COLUMN', @level2name = N'Value';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Cuenta de destino (destino de CXC) en GeneralLedger.MainAccounts. INT. FK. Búsqueda: cuenta destino, GL.', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'PortfolioReclassification', @level2type = N'COLUMN', @level2name = N'TargetAccountId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Destino de la cuenta de la CXC', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'PortfolioReclassification', @level2type = N'COLUMN', @level2name = N'TargetAccountId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'PortfolioReclassification', @level2type = N'COLUMN', @level2name = N'TargetAccountId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Cuenta de origen (origen de CXC) en GeneralLedger.MainAccounts. INT. FK. Búsqueda: cuenta origen, GL.', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'PortfolioReclassification', @level2type = N'COLUMN', @level2name = N'SourceAccountId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Origen de la cuenta de la CXC', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'PortfolioReclassification', @level2type = N'COLUMN', @level2name = N'SourceAccountId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'PortfolioReclassification', @level2type = N'COLUMN', @level2name = N'SourceAccountId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de la Cuenta por Cobrar (CXC) asociada. INT. FK a Portfolio.AccountReceivable. Búsqueda: factura, recaudo.', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'PortfolioReclassification', @level2type = N'COLUMN', @level2name = N'AccountReceivableId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de la cuenta por pagar', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'PortfolioReclassification', @level2type = N'COLUMN', @level2name = N'AccountReceivableId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'PortfolioReclassification', @level2type = N'COLUMN', @level2name = N'AccountReceivableId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tipo de reclasificación: 1=Radicación (público/privado), 2=Devoluciones (privada), 3=Recepción Glosas Subsanables, 4=Conciliaciones, 5=Reiteración, 6=Cuentas Difícil Recaudo. TINYINT.', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'PortfolioReclassification', @level2type = N'COLUMN', @level2name = N'DocumentType';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Tipo de la reclasificacion  1 - Radicacion - Publico y privada  2 - Devoluciones - Privada  3 - Recepcion de Glosas Subsanable - Privada  4 - Conciliaciones - Privada  5 - Reiteracion - Privada  6 - Cuentas de Dificil Recaudo', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'PortfolioReclassification', @level2type = N'COLUMN', @level2name = N'DocumentType';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'PortfolioReclassification', @level2type = N'COLUMN', @level2name = N'DocumentType';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código único identificador de la reclasificación. VARCHAR(20). Búsqueda: número de documento, radicado.', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'PortfolioReclassification', @level2type = N'COLUMN', @level2name = N'Code';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo de la reclasificacion', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'PortfolioReclassification', @level2type = N'COLUMN', @level2name = N'Code';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'PortfolioReclassification', @level2type = N'COLUMN', @level2name = N'Code';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador primario (clave) del registro de reclasificación. INT IDENTITY. Búsqueda: referencia interna.', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'PortfolioReclassification', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de la reclasificacion', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'PortfolioReclassification', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'PortfolioReclassification', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Reclasificaciones de cartera: movimientos contables que trasladan el saldo de una cuenta por cobrar desde una cuenta contable de origen hacia una cuenta destino, registrando el valor reclasificado, el estado del documento (pendiente, confirmado, anulado) y la trazabilidad completa de usuarios y fechas de creación, modificación, confirmación y anulación.', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'PortfolioReclassification';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'PortfolioReclassification';
