CREATE TABLE [Treasury].[CheckCashing] (
    [Id]                      INT           IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [Code]                    VARCHAR (20)  NOT NULL,
    [VoucherTransactionId]    INT           NOT NULL,
    [CurrentCheckNumber]      BIGINT        NOT NULL,
    [NextCheckNumber]         BIGINT        NOT NULL,
    [CancellationDate]        DATE          NOT NULL,
    [CancellationDescription] VARCHAR (300) NOT NULL,
    [CancellationCheckId]     INT           NOT NULL,
    [CreationUser]            VARCHAR (20)  NOT NULL,
    [CreationDate]            DATETIME      NOT NULL,
    [ModificationUser]        VARCHAR (20)  NULL,
    [ModificationDate]        DATETIME      NULL,
    [TimeStamp]               ROWVERSION    NOT NULL,
    CONSTRAINT [PK_CheckCashing] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_CheckCashing_CancellationChecks] FOREIGN KEY ([CancellationCheckId]) REFERENCES [Treasury].[CancellationChecks] ([Id]),
    CONSTRAINT [FK_CheckCashing_VoucherTransaction] FOREIGN KEY ([VoucherTransactionId]) REFERENCES [Treasury].[VoucherTransaction] ([Id])
);




GO



GO



GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Marca temporal (TIMESTAMP SQL Server) del evento de cambio de cheque; registra el instante exacto de creación, modificación o cancelación del registro en la base de datos.', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'CheckCashing', @level2type = N'COLUMN', @level2name = N'TimeStamp';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Marca temporal en la que ocurrio determinado evento. Guarda el instante tiempo de la creacion , registro o modificacion de un archivo determinado.', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'CheckCashing', @level2type = N'COLUMN', @level2name = N'TimeStamp';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'CheckCashing', @level2type = N'COLUMN', @level2name = N'TimeStamp';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora de la última modificación (DATETIME) del registro de cambio de cheque; nula si no ha sido editado tras su creación.', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'CheckCashing', @level2type = N'COLUMN', @level2name = N'ModificationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha Modificación', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'CheckCashing', @level2type = N'COLUMN', @level2name = N'ModificationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'CheckCashing', @level2type = N'COLUMN', @level2name = N'ModificationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Usuario (VARCHAR 20) que realizó la última modificación del cambio de cheque; referencia al login o identificador del operario de tesorería.', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'CheckCashing', @level2type = N'COLUMN', @level2name = N'ModificationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Usuario Modificación', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'CheckCashing', @level2type = N'COLUMN', @level2name = N'ModificationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'CheckCashing', @level2type = N'COLUMN', @level2name = N'ModificationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora de creación (DATETIME) del registro de cambio de cheque; marca cuándo se registró el evento de sustitución en el sistema.', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'CheckCashing', @level2type = N'COLUMN', @level2name = N'CreationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha de Creación', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'CheckCashing', @level2type = N'COLUMN', @level2name = N'CreationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'CheckCashing', @level2type = N'COLUMN', @level2name = N'CreationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Usuario (VARCHAR 20) que creó el registro del cambio de cheque; operario o sistema que generó el evento de sustitución.', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'CheckCashing', @level2type = N'COLUMN', @level2name = N'CreationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Usuario Creación', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'CheckCashing', @level2type = N'COLUMN', @level2name = N'CreationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'CheckCashing', @level2type = N'COLUMN', @level2name = N'CreationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador (INT, FK) del cheque cancelado en Treasury.CancellationChecks; relaciona la razón y detalles de la cancelación del cheque original.', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'CheckCashing', @level2type = N'COLUMN', @level2name = N'CancellationCheckId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de la cancelacion del cheque', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'CheckCashing', @level2type = N'COLUMN', @level2name = N'CancellationCheckId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'CheckCashing', @level2type = N'COLUMN', @level2name = N'CancellationCheckId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Descripción textual (VARCHAR 300) del motivo de cancelación del cheque; explica la razón por la cual se sustituyó (cheque perdido, dañado, no cobrado, etc.).', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'CheckCashing', @level2type = N'COLUMN', @level2name = N'CancellationDescription';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Descripcion de la cancelacion del cheque', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'CheckCashing', @level2type = N'COLUMN', @level2name = N'CancellationDescription';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'CheckCashing', @level2type = N'COLUMN', @level2name = N'CancellationDescription';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha (DATE) en que se canceló el cheque original; marca cuándo se efectuó formalmente la invalidación del comprobante anterior.', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'CheckCashing', @level2type = N'COLUMN', @level2name = N'CancellationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha de cancelacion del cheque', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'CheckCashing', @level2type = N'COLUMN', @level2name = N'CancellationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'CheckCashing', @level2type = N'COLUMN', @level2name = N'CancellationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número del nuevo cheque (BIGINT) que reemplaza al anterior; identificador secuencial del cheque sustituto emitido.', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'CheckCashing', @level2type = N'COLUMN', @level2name = N'NextCheckNumber';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Numero del nuevo cheque', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'CheckCashing', @level2type = N'COLUMN', @level2name = N'NextCheckNumber';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'CheckCashing', @level2type = N'COLUMN', @level2name = N'NextCheckNumber';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número del cheque anterior o actual (BIGINT) que se reemplaza; identificador secuencial del comprobante original invalidado.', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'CheckCashing', @level2type = N'COLUMN', @level2name = N'CurrentCheckNumber';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Numero del cheque anterior', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'CheckCashing', @level2type = N'COLUMN', @level2name = N'CurrentCheckNumber';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'CheckCashing', @level2type = N'COLUMN', @level2name = N'CurrentCheckNumber';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador (INT, FK) del comprobante de egreso en Treasury.VoucherTransaction; transacción de tesorería que cambia de cheque.', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'CheckCashing', @level2type = N'COLUMN', @level2name = N'VoucherTransactionId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del comprobante de egreso, el cual se va cambiar de cheque', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'CheckCashing', @level2type = N'COLUMN', @level2name = N'VoucherTransactionId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'CheckCashing', @level2type = N'COLUMN', @level2name = N'VoucherTransactionId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código único (VARCHAR 20) del cambio de cheque; identificador alfanumérico que permite rastrear la sustitución en auditoría y reportes.', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'CheckCashing', @level2type = N'COLUMN', @level2name = N'Code';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo del cambio de cheque', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'CheckCashing', @level2type = N'COLUMN', @level2name = N'Code';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'CheckCashing', @level2type = N'COLUMN', @level2name = N'Code';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único (INT IDENTITY, PK) del registro de cambio de cheque; clave primaria que identifica la transacción de sustitución.', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'CheckCashing', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del cambio de cheque', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'CheckCashing', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'CheckCashing', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Registro de cancelación y reemplazo de cheques en tesorería. Guarda el historial de cheques anulados, indicando el número de cheque cancelado, el nuevo número asignado como reemplazo, el motivo de la cancelación y la transacción de comprobante asociada.', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'CheckCashing';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'CheckCashing';
