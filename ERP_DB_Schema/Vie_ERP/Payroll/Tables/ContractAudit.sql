CREATE TABLE [Payroll].[ContractAudit](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[ContractId] [int] NOT NULL,
	[Type] [varchar](100) NOT NULL,
	[FieldName] [varchar](100) NOT NULL,
	[ValueOld] [varchar](max) NULL,
	[ValueNew] [varchar](max) NULL,
	[UserCode] [varchar](20) NOT NULL,
	[Date] [datetime] NOT NULL,
 CONSTRAINT [PK_ContractAudit] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Id de la tabla de auditoría' , @level0type=N'SCHEMA',@level0name=N'Payroll', @level1type=N'TABLE',@level1name=N'ContractAudit', @level2type=N'COLUMN',@level2name=N'Id'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Id del contrato modificado' , @level0type=N'SCHEMA',@level0name=N'Payroll', @level1type=N'TABLE',@level1name=N'ContractAudit', @level2type=N'COLUMN',@level2name=N'ContractId'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Tipo de campo modificado (ej: unidad funcional, Salario, etc.)' , @level0type=N'SCHEMA',@level0name=N'Payroll', @level1type=N'TABLE',@level1name=N'ContractAudit', @level2type=N'COLUMN',@level2name=N'Type'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Campo afectado de contrato' , @level0type=N'SCHEMA',@level0name=N'Payroll', @level1type=N'TABLE',@level1name=N'ContractAudit', @level2type=N'COLUMN',@level2name=N'FieldName'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Valor anterior del campo modificado' , @level0type=N'SCHEMA',@level0name=N'Payroll', @level1type=N'TABLE',@level1name=N'ContractAudit', @level2type=N'COLUMN',@level2name=N'ValueOld'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Nuevo valor del campo modificado' , @level0type=N'SCHEMA',@level0name=N'Payroll', @level1type=N'TABLE',@level1name=N'ContractAudit', @level2type=N'COLUMN',@level2name=N'ValueNew'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Usuario que realizó la modificación' , @level0type=N'SCHEMA',@level0name=N'Payroll', @level1type=N'TABLE',@level1name=N'ContractAudit', @level2type=N'COLUMN',@level2name=N'UserCode'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Fecha de modificación del cambio' , @level0type=N'SCHEMA',@level0name=N'Payroll', @level1type=N'TABLE',@level1name=N'ContractAudit', @level2type=N'COLUMN',@level2name=N'Date'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Tabla de auditoría genérica que registra los cambios realizados en diferentes campos de la tabla Contract' , @level0type=N'SCHEMA',@level0name=N'Payroll', @level1type=N'TABLE',@level1name=N'ContractAudit'
GO