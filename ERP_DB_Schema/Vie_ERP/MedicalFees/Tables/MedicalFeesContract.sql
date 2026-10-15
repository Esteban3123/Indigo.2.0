CREATE TABLE [MedicalFees].[MedicalFeesContract] (
    [Id]                             INT           IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [Code]                           VARCHAR (20)  NOT NULL,
    [ContractType]                   TINYINT       NOT NULL,
    [SupplierId]                     INT           NULL,
    [SupplierDistributionLineId]     INT           NULL,
    [ContractName]                   VARCHAR (100) NOT NULL,
    [ContractNumber]                 VARCHAR (15)  NULL,
    [InitialDate]                    DATETIME      NOT NULL,
    [EndDate]                        DATETIME      NOT NULL,
    [LastLiquidationDate]            DATETIME      CONSTRAINT [DF_MedicalFeesContract_LiquidationLastDate] DEFAULT ([Common].[getdate]()) NOT NULL,
    [LastLiquidationDateTmp]         DATETIME      CONSTRAINT [DF_MedicalFeesContract_LastLiquidationDate1] DEFAULT ([Common].[getdate]()) NULL,
    [AutomaticDiscountForObjections] BIT           CONSTRAINT [DF_MedicalFeesContract_AutomaticDiscountForObjections] DEFAULT ((0)) NOT NULL,
    [Observations]                   VARCHAR (MAX) NULL,
    [Status]                         TINYINT       NOT NULL,
    [CreationUser]                   VARCHAR (20)  NOT NULL,
    [CreationDate]                   DATETIME      NOT NULL,
    [ModificationUser]               VARCHAR (20)  NULL,
    [ModificationDate]               DATETIME      NULL,
    [InForceUser]                    VARCHAR (20)  NULL,
    [InForceDate]                    DATETIME      NULL,
    [SuspendedUser]                  VARCHAR (20)  NULL,
    [SuspendedDate]                  DATETIME      NULL,
    [FinishedUser]                   VARCHAR (20)  NULL,
    [FinishedDate]                   DATETIME      NULL,
    [TimeStamp]                      ROWVERSION    NOT NULL,
    CONSTRAINT [PK_MedicalFeesContract] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_MedicalFeesContract_Supplier] FOREIGN KEY ([SupplierId]) REFERENCES [Common].[Supplier] ([Id]),
    CONSTRAINT [FK_MedicalFeesContract_SuppliersDistributionLines] FOREIGN KEY ([SupplierDistributionLineId]) REFERENCES [Common].[SuppliersDistributionLines] ([Id])
);


GO
ALTER TABLE [MedicalFees].[MedicalFeesContract] NOCHECK CONSTRAINT [FK_MedicalFeesContract_Supplier];


GO
ALTER TABLE [MedicalFees].[MedicalFeesContract] NOCHECK CONSTRAINT [FK_MedicalFeesContract_SuppliersDistributionLines];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Marca temporal en la que ocurrio determinado evento. Guarda el instante tiempo de la creacion , registro o modificacion de un archivo determinado.', @level0type = N'SCHEMA', @level0name = N'MedicalFees', @level1type = N'TABLE', @level1name = N'MedicalFeesContract', @level2type = N'COLUMN', @level2name = N'TimeStamp';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha en que finalizo el contrato', @level0type = N'SCHEMA', @level0name = N'MedicalFees', @level1type = N'TABLE', @level1name = N'MedicalFeesContract', @level2type = N'COLUMN', @level2name = N'FinishedDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Usuario que finalizo el contrato', @level0type = N'SCHEMA', @level0name = N'MedicalFees', @level1type = N'TABLE', @level1name = N'MedicalFeesContract', @level2type = N'COLUMN', @level2name = N'FinishedUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha en que se suspendio el contrato', @level0type = N'SCHEMA', @level0name = N'MedicalFees', @level1type = N'TABLE', @level1name = N'MedicalFeesContract', @level2type = N'COLUMN', @level2name = N'SuspendedDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Usuario que suspendio el contrato', @level0type = N'SCHEMA', @level0name = N'MedicalFees', @level1type = N'TABLE', @level1name = N'MedicalFeesContract', @level2type = N'COLUMN', @level2name = N'SuspendedUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha en que se cambio a estado vigente', @level0type = N'SCHEMA', @level0name = N'MedicalFees', @level1type = N'TABLE', @level1name = N'MedicalFeesContract', @level2type = N'COLUMN', @level2name = N'InForceDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Usuario que cambio a estado vigente', @level0type = N'SCHEMA', @level0name = N'MedicalFees', @level1type = N'TABLE', @level1name = N'MedicalFeesContract', @level2type = N'COLUMN', @level2name = N'InForceUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha Modificación', @level0type = N'SCHEMA', @level0name = N'MedicalFees', @level1type = N'TABLE', @level1name = N'MedicalFeesContract', @level2type = N'COLUMN', @level2name = N'ModificationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Usuario Modificación', @level0type = N'SCHEMA', @level0name = N'MedicalFees', @level1type = N'TABLE', @level1name = N'MedicalFeesContract', @level2type = N'COLUMN', @level2name = N'ModificationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha de Creación', @level0type = N'SCHEMA', @level0name = N'MedicalFees', @level1type = N'TABLE', @level1name = N'MedicalFeesContract', @level2type = N'COLUMN', @level2name = N'CreationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Usuario Creación', @level0type = N'SCHEMA', @level0name = N'MedicalFees', @level1type = N'TABLE', @level1name = N'MedicalFeesContract', @level2type = N'COLUMN', @level2name = N'CreationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Estado del contrato  1 - Vigente  2 - Suspendido  3 - Terminado', @level0type = N'SCHEMA', @level0name = N'MedicalFees', @level1type = N'TABLE', @level1name = N'MedicalFeesContract', @level2type = N'COLUMN', @level2name = N'Status';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Observaciones del contrato', @level0type = N'SCHEMA', @level0name = N'MedicalFees', @level1type = N'TABLE', @level1name = N'MedicalFeesContract', @level2type = N'COLUMN', @level2name = N'Observations';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica si se realiza un descuento al medico por las aceptaciones de la IPS, cuando corresponde a la responsabilidad exclusiva del profesional de la salud.    Cuando el contrato asociado a la causacion especifica el descuento automatico el sistema de glosas preguntará si el valor aceptado es responsabilidad exclusiva del profesional, de ser positiva la respuesta, el sistema pondra el campo ObjectionAcepted en verdadero para su futuro descuento.', @level0type = N'SCHEMA', @level0name = N'MedicalFees', @level1type = N'TABLE', @level1name = N'MedicalFeesContract', @level2type = N'COLUMN', @level2name = N'AutomaticDiscountForObjections';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica la fecha de la ultima liquidaicon de un documento cuando no esta confirmado, Esta fecha se actualiza cuando se guarda un documento de liquidacion y cuando se anula o se confirma este campo vuelve a quedar nulo', @level0type = N'SCHEMA', @level0name = N'MedicalFees', @level1type = N'TABLE', @level1name = N'MedicalFeesContract', @level2type = N'COLUMN', @level2name = N'LastLiquidationDateTmp';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica la fecha de la ultima liquidaicon, no puede ser nula, superior a la fecha inicial del contrato e inferior o igual a la fecha actual, se actualiza en cada confirmacion de liquidacion', @level0type = N'SCHEMA', @level0name = N'MedicalFees', @level1type = N'TABLE', @level1name = N'MedicalFeesContract', @level2type = N'COLUMN', @level2name = N'LastLiquidationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha Final del contrato', @level0type = N'SCHEMA', @level0name = N'MedicalFees', @level1type = N'TABLE', @level1name = N'MedicalFeesContract', @level2type = N'COLUMN', @level2name = N'EndDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha inicial del contrato', @level0type = N'SCHEMA', @level0name = N'MedicalFees', @level1type = N'TABLE', @level1name = N'MedicalFeesContract', @level2type = N'COLUMN', @level2name = N'InitialDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Numero del contrato', @level0type = N'SCHEMA', @level0name = N'MedicalFees', @level1type = N'TABLE', @level1name = N'MedicalFeesContract', @level2type = N'COLUMN', @level2name = N'ContractNumber';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Nombre del contrato', @level0type = N'SCHEMA', @level0name = N'MedicalFees', @level1type = N'TABLE', @level1name = N'MedicalFeesContract', @level2type = N'COLUMN', @level2name = N'ContractName';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id de la linea de distribucion, Esta solo se habilita si el tipo de contrato es de agremiaciones ', @level0type = N'SCHEMA', @level0name = N'MedicalFees', @level1type = N'TABLE', @level1name = N'MedicalFeesContract', @level2type = N'COLUMN', @level2name = N'SupplierDistributionLineId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del proveedor, Este campo solo se habilita cuando sea agremiaciones    Este campo es Utilizado en el momento de hacer la liquidacion de honorartios medicos, Cuando es una agremiacion la CXP saldra a nombre del proveedor que hayan ingresado en este campo', @level0type = N'SCHEMA', @level0name = N'MedicalFees', @level1type = N'TABLE', @level1name = N'MedicalFeesContract', @level2type = N'COLUMN', @level2name = N'SupplierId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica el tipo de contrato  1 - Estandar  2 - Agremiaciones    Estandar:  No se solicita el tercero, se toma del medico, sirve para cuando establecer las tarifas de un medico en particular o cuando varios medicos tienen la misma tarifa.    Agremiaciones:  Solicita el tercero y permite asociar varios medicos al mismo contrato que al momento de liquidar se causaran a un solo nit.', @level0type = N'SCHEMA', @level0name = N'MedicalFees', @level1type = N'TABLE', @level1name = N'MedicalFeesContract', @level2type = N'COLUMN', @level2name = N'ContractType';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo del contrato', @level0type = N'SCHEMA', @level0name = N'MedicalFees', @level1type = N'TABLE', @level1name = N'MedicalFeesContract', @level2type = N'COLUMN', @level2name = N'Code';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id de la Tabla', @level0type = N'SCHEMA', @level0name = N'MedicalFees', @level1type = N'TABLE', @level1name = N'MedicalFeesContract', @level2type = N'COLUMN', @level2name = N'Id';

