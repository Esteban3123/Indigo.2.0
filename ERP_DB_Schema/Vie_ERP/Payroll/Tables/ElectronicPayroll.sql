CREATE TABLE [Payroll].[ElectronicPayroll] (
    [Id]              INT           IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [DocumentType]    TINYINT       NOT NULL,
    [Year]            INT           NOT NULL,
    [Consecutive]     INT           NOT NULL,
    [Month]           TINYINT       NOT NULL,
    [EmployeePartyId] INT           NOT NULL,
    [EntityName]      VARCHAR (250) NOT NULL,
    [EntityId]        INT           NOT NULL,
    [Prefix]          VARCHAR (5)   NULL,
    [DocumentNumber]  VARCHAR (20)  NOT NULL,
    [CUNE]            VARCHAR (500) NULL,
    [Status]          TINYINT       NOT NULL,
    [CreationDate]    DATETIME      NOT NULL,
    [ShippingDate]    DATETIME      NULL,
    [ValidationDate]  DATETIME      NULL,
    [FilePath]        VARCHAR (250) NOT NULL,
    [Retry]           INT           NOT NULL,
    [ZipKey]          VARCHAR (500) NULL,
    [ContractId]      INT           NULL,
    CONSTRAINT [PK_ElectronicPayroll] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_ElectronicPayroll_Contract] FOREIGN KEY ([ContractId]) REFERENCES [Payroll].[Contract] ([Id]),
    CONSTRAINT [FK_ElectronicPayroll_ThirdParty] FOREIGN KEY ([EmployeePartyId]) REFERENCES [Common].[ThirdParty] ([Id])
);


GO
ALTER TABLE [Payroll].[ElectronicPayroll] NOCHECK CONSTRAINT [FK_ElectronicPayroll_Contract];


GO
ALTER TABLE [Payroll].[ElectronicPayroll] NOCHECK CONSTRAINT [FK_ElectronicPayroll_ThirdParty];


GO

CREATE TRIGGER [Payroll].[trg_AfterInsertElectronicPayrollDocumentNumber]
ON [Payroll].[ElectronicPayroll] 
AFTER INSERT
AS
BEGIN
    DECLARE @DuplicateCount INT;
    DECLARE @DocumentNumber NVARCHAR(20); -- Asegúrate de que el tipo de dato coincida con el de tu columna DocumentNumber
    
    -- Obtener el DocumentNumber del registro insertado
    SELECT @DocumentNumber = DocumentNumber
    FROM inserted;
    
    -- Verificar si ya existe un documento con el mismo número
    SELECT @DuplicateCount = COUNT(*)
    FROM PAYROLL.ElectronicPayroll EP
    JOIN inserted i ON EP.DocumentNumber = i.DocumentNumber;

    -- Si hay más de un documento con el mismo número, generar un error
    IF @DuplicateCount > 1
    BEGIN
        -- Si el documento ya existe, generar un error y eliminar el registro insertado
        RAISERROR('El documento electronico con el número %s ya existe y no se puede insertar duplicado.', 16, 1, @DocumentNumber);
        ROLLBACK TRANSACTION; -- Revertir la inserción
    END
END
GO

ENABLE TRIGGER [Payroll].[trg_AfterInsertElectronicPayrollDocumentNumber]
ON [Payroll].[ElectronicPayroll];

GO
-- =============================================
-- Author:		Miguel Angel Fonseca Castro
-- Create date: 2021-08-18
-- Description:	Trigger para que genere automáticamente el consecutivo del documento electrónico de nomina
-- =============================================
CREATE TRIGGER [Payroll].[tggElectronicPayrollGenerateConsecutive]
   ON  [Payroll].[ElectronicPayroll]
   AFTER INSERT
AS 
BEGIN
	SET NOCOUNT ON

	DECLARE @Rows INT = 1,
			@Id INT = 0

	WHILE @Rows > 0
	BEGIN
		SELECT TOP 1
			@Id = Id
		FROM INSERTED
		WHERE Id > @Id
		ORDER BY Id

		SET @Rows = @@ROWCOUNT
		IF @Rows = 0 
		BEGIN
			BREAK
		END

		--------------------------------------------

		UPDATE ep
			SET ep.Consecutive = ISNULL
			(
				(
					SELECT MAX(ed.Consecutive)
					FROM Payroll.ElectronicPayroll ed 
					WHERE ed.Year = ep.Year AND  ed.DocumentType = ep.DocumentType
				), 0
			) + 1
		FROM Payroll.ElectronicPayroll ep
		WHERE ep.Id = @Id
	END
END
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Id del contrato', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'ElectronicPayroll', @level2type = N'COLUMN', @level2name = N'ContractId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Corresponde al número generado una vez concluida exitosamente las validaciones iniciales y los documentos pasan a la cola de validación.', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'ElectronicPayroll', @level2type = N'COLUMN', @level2name = N'ZipKey';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Número de intentos de envío', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'ElectronicPayroll', @level2type = N'COLUMN', @level2name = N'Retry';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Ruta donde se encuentra el archivo físico', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'ElectronicPayroll', @level2type = N'COLUMN', @level2name = N'FilePath';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha de validación ante la DIAN (Fecha en que cambia del estado Enviado a Válido o Inválido)', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'ElectronicPayroll', @level2type = N'COLUMN', @level2name = N'ValidationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha de envío a la DIAN (Fecha en que cambia del estado Registrado a Enviado)', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'ElectronicPayroll', @level2type = N'COLUMN', @level2name = N'ShippingDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha de creación del registro', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'ElectronicPayroll', @level2type = N'COLUMN', @level2name = N'CreationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Determina el estado actual del registro ante la DIAN      0. Invalida      1. Registrada      2. Enviada      3. Validada      4. Validacion Fallida      ----------------------------------------------      66. Reenvío Obligatorio      88. Pendiente', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'ElectronicPayroll', @level2type = N'COLUMN', @level2name = N'Status';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Código Unico del Documento Electronico', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'ElectronicPayroll', @level2type = N'COLUMN', @level2name = N'CUNE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Numero del documento sin Prefijo', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'ElectronicPayroll', @level2type = N'COLUMN', @level2name = N'DocumentNumber';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Prefijo', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'ElectronicPayroll', @level2type = N'COLUMN', @level2name = N'Prefix';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del documento que genero el registro', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'ElectronicPayroll', @level2type = N'COLUMN', @level2name = N'EntityId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Nombre de la entidad desde la cual se genero el registro', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'ElectronicPayroll', @level2type = N'COLUMN', @level2name = N'EntityName';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Tercero del empleado', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'ElectronicPayroll', @level2type = N'COLUMN', @level2name = N'EmployeePartyId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Mes o Periodo de reporte del documento', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'ElectronicPayroll', @level2type = N'COLUMN', @level2name = N'Month';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Consecutivo del documento electrónico en la vigencia', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'ElectronicPayroll', @level2type = N'COLUMN', @level2name = N'Consecutive';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Año del documento', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'ElectronicPayroll', @level2type = N'COLUMN', @level2name = N'Year';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Tipos de Documentos      1. Documento Soporte de Pago de Nómina Electrónica      2. Notas de Ajuste      3. Nómina Electrónica Reportada  ', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'ElectronicPayroll', @level2type = N'COLUMN', @level2name = N'DocumentType';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del documento de nómina electronica', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'ElectronicPayroll', @level2type = N'COLUMN', @level2name = N'Id';

