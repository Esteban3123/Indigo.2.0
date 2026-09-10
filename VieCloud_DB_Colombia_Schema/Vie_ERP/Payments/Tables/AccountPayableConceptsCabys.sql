CREATE TABLE [Payments].[AccountPayableConceptsCabys](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[AccountPayableConceptId] [int] NOT NULL,
	[CabysCode] [varchar](20) NOT NULL,
	[CabysDescription] [nvarchar](255) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [Payments].[AccountPayableConceptsCabys]  WITH CHECK ADD  CONSTRAINT [FK_AccountPayableConceptsCabys_Concept] FOREIGN KEY([AccountPayableConceptId])
REFERENCES [Payments].[AccountPayableConcepts] ([Id])
GO

ALTER TABLE [Payments].[AccountPayableConceptsCabys] CHECK CONSTRAINT [FK_AccountPayableConceptsCabys_Concept]
GO


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Relaciona los conceptos de cuentas por pagar con sus códigos CABYS (Catálogo de Bienes y Servicios de Costa Rica), permitiendo clasificar cada concepto de pago según la nomenclatura fiscal oficial.', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'AccountPayableConceptsCabys';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'AccountPayableConceptsCabys';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único interno del registro de asociación entre concepto de pago y código CABYS.', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'AccountPayableConceptsCabys', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'AccountPayableConceptsCabys', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Referencia al concepto de cuenta por pagar al que se le asigna el código CABYS.', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'AccountPayableConceptsCabys', @level2type = N'COLUMN', @level2name = N'AccountPayableConceptId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'AccountPayableConceptsCabys', @level2type = N'COLUMN', @level2name = N'AccountPayableConceptId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código CABYS del bien o servicio según el catálogo fiscal oficial de Costa Rica, usado para facturación electrónica.', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'AccountPayableConceptsCabys', @level2type = N'COLUMN', @level2name = N'CabysCode';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'AccountPayableConceptsCabys', @level2type = N'COLUMN', @level2name = N'CabysCode';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Descripción oficial del bien o servicio según el catálogo CABYS, nombre del producto o servicio clasificado.', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'AccountPayableConceptsCabys', @level2type = N'COLUMN', @level2name = N'CabysDescription';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'AccountPayableConceptsCabys', @level2type = N'COLUMN', @level2name = N'CabysDescription';
