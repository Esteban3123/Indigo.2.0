CREATE TABLE [dbo].[ADFURIPSInvoice](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[ADFURIPSId] [int] NOT NULL,
	[InvoiceId] [int] NOT NULL,
	[StatusFrom] [int] NULL,
	[StatusTo] [int] NULL,
	[UpdatedBy] [varchar](50) NULL,
	[UpdatedDate] [datetime] NULL,
 CONSTRAINT [PK_ADFURIPSInvoice] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[ADFURIPSInvoice]  WITH CHECK ADD  CONSTRAINT [FK_ADFURIPS_ADFURIPSInvoice] FOREIGN KEY([ADFURIPSId])
REFERENCES [dbo].[ADFURIPS] ([Id])
GO

ALTER TABLE [dbo].[ADFURIPSInvoice] CHECK CONSTRAINT [FK_ADFURIPS_ADFURIPSInvoice]
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Consecutivo de la tabla' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ADFURIPSInvoice', @level2type=N'COLUMN',@level2name=N'Id'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Identificador de la tabla ADFURIPS' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ADFURIPSInvoice', @level2type=N'COLUMN',@level2name=N'ADFURIPSId'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Identificador de la tabla factura asociada al FUR' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ADFURIPSInvoice', @level2type=N'COLUMN',@level2name=N'InvoiceId'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Estado inicial del movimiento del FUR: 0. Sin afectar, 1. Confirmado, 2. Desconfirmado' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ADFURIPSInvoice', @level2type=N'COLUMN',@level2name=N'StatusFrom'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Estado final del movimiento del FUR: 0. Sin afectar, 1. Confirmado, 2. Desconfirmado' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ADFURIPSInvoice', @level2type=N'COLUMN',@level2name=N'StatusTo'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Usuario que genera el registro' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ADFURIPSInvoice', @level2type=N'COLUMN',@level2name=N'UpdatedBy'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Fecha en la que se genera el registro' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ADFURIPSInvoice', @level2type=N'COLUMN',@level2name=N'UpdatedDate'
GO


