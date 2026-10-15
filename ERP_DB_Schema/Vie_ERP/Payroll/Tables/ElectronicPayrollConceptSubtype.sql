

CREATE TABLE [Payroll].[ElectronicPayrollConceptSubtype](
	[Id] INT IDENTITY(1,1) NOT NULL,
	[IdElectronicPayrollConcepts] INT NOT NULL,
	[Name] VARCHAR (100) NOT NULL,
	[InternalSubCode] TINYINT NOT NULL,
	[State] BIT NOT NULL,
    CONSTRAINT [PK_ElectronicPayrollConceptSubtype_Id] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_ElectronicPayrollConceptSubtype_ElectronicPayrollConcepts] FOREIGN KEY([IdElectronicPayrollConcepts]) REFERENCES [Payroll].[ElectronicPayrollConcepts] ([Id])
);

GO
ALTER TABLE [Payroll].[ElectronicPayrollConceptSubtype] ADD  DEFAULT ((1)) FOR [State];


GO
ALTER TABLE [Payroll].[ElectronicPayrollConceptSubtype] CHECK CONSTRAINT [FK_ElectronicPayrollConceptSubtype_ElectronicPayrollConcepts];




GO
EXECUTE sp_addextendedproperty 
    @name = N'MS_Description',
    @value = N'Id relacion con ElectronicPayrollConcepts',
    @level0type = N'SCHEMA', @level0name = 'Payroll',
    @level1type = N'TABLE',  @level1name = 'ElectronicPayrollConceptSubtype',
    @level2type = N'COLUMN', @level2name = 'IdElectronicPayrollConcepts';

GO
EXECUTE sp_addextendedproperty 
    @name = N'MS_Description',
    @value = N'Nombre del tipo de sub-tipo ',
    @level0type = N'SCHEMA', @level0name = 'Payroll',
    @level1type = N'TABLE',  @level1name = 'ElectronicPayrollConceptSubtype',
    @level2type = N'COLUMN', @level2name = 'Name';


GO
EXECUTE sp_addextendedproperty 
    @name = N'MS_Description',
    @value = N'Código interno del subtipo . Este valor se envía en el XML DIAN para discriminar variaciones dentro del mismo Type.',
    @level0type = N'SCHEMA', @level0name = 'Payroll',
    @level1type = N'TABLE',  @level1name = 'ElectronicPayrollConceptSubtype',
    @level2type = N'COLUMN', @level2name = 'InternalSubCode';
