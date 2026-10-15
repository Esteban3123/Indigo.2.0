CREATE TABLE [GeneralLedger].[MainAccounts] (
    [Id]                           INT           IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [LegalBookId]                  INT           NOT NULL,
    [IdAccountLevel]               INT           NOT NULL,
    [IdAccountClass]               INT           NOT NULL,
    [Number]                       VARCHAR (50)  NOT NULL,
    [Nature]                       TINYINT       NULL,
    [Name]                         VARCHAR (100) NOT NULL,
    [IdParent]                     INT           NULL,
    [HandlesThirdParty]            BIT           CONSTRAINT [DF_MainAccount_HandlesThirdParty] DEFAULT ((0)) NOT NULL,
    [CloseThirdParty]              BIT           CONSTRAINT [DF_MainAccount_CloseThird] DEFAULT ((0)) NOT NULL,
    [IdThirdParty]                 INT           CONSTRAINT [DF_MainAccount_ThirdId] DEFAULT ((0)) NULL,
    [ReconcileAccount]             BIT           CONSTRAINT [DF_MainAccount_ReconcileAccount] DEFAULT ((0)) NOT NULL,
    [Availability]                 TINYINT       NOT NULL,
    [ShowCGN2]                     BIT           CONSTRAINT [DF_MainAccounts_ShowCGN2] DEFAULT ((1)) NOT NULL,
    [HandlesCostCenter]            BIT           CONSTRAINT [DF_MainAccount_HandlesCostCenter] DEFAULT ((0)) NOT NULL,
    [RetencionType]                TINYINT       CONSTRAINT [DF_MainAccount_RetencionType] DEFAULT ((0)) NOT NULL,
    [FreelancerCategory]           BIT           CONSTRAINT [DF_MainAccounts_FreelancerCategory_1] DEFAULT ((0)) NOT NULL,
    [AllowsMovement]               BIT           NOT NULL,
    [Status]                       BIT           CONSTRAINT [DF_MainAccount_AccountActive] DEFAULT ((1)) NOT NULL,
    [CreationUser]                 VARCHAR (20)  NOT NULL,
    [CreationDate]                 DATETIME      NOT NULL,
    [ModificationUser]             VARCHAR (20)  NULL,
    [ModificationDate]             DATETIME      NULL,
    [TimeStamp]                    ROWVERSION    NOT NULL,
    [HandleBase]                   BIT           CONSTRAINT [DF__MainAccou__Handl__1808D8A9] DEFAULT ((0)) NOT NULL,
    [HandlesThirdPartyRestriction] BIT           CONSTRAINT [DF__MainAccou__Handl__34A5E2AE] DEFAULT ((0)) NOT NULL,
    [HandlesCostCenterRestriction] BIT           CONSTRAINT [DF__MainAccou__Handl__359A06E7] DEFAULT ((0)) NOT NULL,
    CONSTRAINT [PK_MainAccounts__Id] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_MainAccounts_LegalBook] FOREIGN KEY ([LegalBookId]) REFERENCES [GeneralLedger].[LegalBook] ([Id]),
    CONSTRAINT [FK_MainAccounts_MainAccounts] FOREIGN KEY ([IdParent]) REFERENCES [GeneralLedger].[MainAccounts] ([Id]),
    CONSTRAINT [FK_MainAccounts_ThirdParty] FOREIGN KEY ([IdThirdParty]) REFERENCES [Common].[ThirdParty] ([Id]) NOT FOR REPLICATION,
    CONSTRAINT [FK_PUC_AccountClass] FOREIGN KEY ([IdAccountClass]) REFERENCES [GeneralLedger].[MainAccountClasses] ([Id]),
    CONSTRAINT [FK_PUC_AccountLevel] FOREIGN KEY ([IdAccountLevel]) REFERENCES [GeneralLedger].[MainAccountLevels] ([Id]),
    CONSTRAINT [UQ_MainAccounts__LegalBookId__Number] UNIQUE NONCLUSTERED ([LegalBookId] ASC, [Number] ASC)
);


GO
ALTER TABLE [GeneralLedger].[MainAccounts] NOCHECK CONSTRAINT [FK_MainAccounts_LegalBook];


GO
ALTER TABLE [GeneralLedger].[MainAccounts] NOCHECK CONSTRAINT [FK_MainAccounts_MainAccounts];


GO
ALTER TABLE [GeneralLedger].[MainAccounts] NOCHECK CONSTRAINT [FK_MainAccounts_ThirdParty];


GO
ALTER TABLE [GeneralLedger].[MainAccounts] NOCHECK CONSTRAINT [FK_PUC_AccountClass];


GO
ALTER TABLE [GeneralLedger].[MainAccounts] NOCHECK CONSTRAINT [FK_PUC_AccountLevel];


GO
CREATE NONCLUSTERED INDEX [IX_MainAccounts_Number]
    ON [GeneralLedger].[MainAccounts]([Number] ASC)
    INCLUDE([Id], [Name], [Nature]);


GO
CREATE NONCLUSTERED INDEX [IX_MainAccounts__RetencionType__INC__Id]
    ON [GeneralLedger].[MainAccounts]([RetencionType] ASC)
    INCLUDE([Id]);


GO

CREATE TRIGGER [GeneralLedger].[tgg_CheckIdParent]
ON [GeneralLedger].[MainAccounts]
AFTER INSERT
AS
BEGIN
    DECLARE @idAccountLevel INT
    DECLARE @idParent INT

    -- Obtener los valores de las columnas que se van a insertar
    SELECT @idAccountLevel = inserted.IdAccountLevel, @idParent = inserted.IdParent
    FROM inserted

    -- Verificar la condición
    IF (@idAccountLevel > 1 AND @idParent IS NULL)
    BEGIN
        -- Lanzar un error personalizado
        ROLLBACK TRANSACTION;
        THROW 51000, 'No se puede insertar una cuenta con IdAccountLevel > 1 y IdParent nulo', 1;
    END

    ELSE
    BEGIN
		-- Actualizar la cuenta padre para establecer AllowsMovement en 0
        IF @idParent IS NOT NULL
        BEGIN
            UPDATE GeneralLedger.MainAccounts
            SET AllowsMovement = 0
            WHERE Id = @idParent;
        END
    END
END;
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '   0: no tiene restricciones de centro de costos   1: Restringe todos los centros de costos excepto los agregados en la tabla [GeneralLedger].[MainAccountRestrictions]   ', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'MainAccounts', @level2type = N'COLUMN', @level2name = N'HandlesCostCenterRestriction';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '   0: no tiene restricciones de terceros   1: Restringe todos los terceros y habilita solo los agregados en la tabla [GeneralLedger].[MainAccountRestrictions]   ', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'MainAccounts', @level2type = N'COLUMN', @level2name = N'HandlesThirdPartyRestriction';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Maneja Base', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'MainAccounts', @level2type = N'COLUMN', @level2name = N'HandleBase';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Marca temporal en la que ocurrio determinado evento. Guarda el instante tiempo de la creacion , registro o modificacion de un archivo determinado.', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'MainAccounts', @level2type = N'COLUMN', @level2name = N'TimeStamp';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha Modificación', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'MainAccounts', @level2type = N'COLUMN', @level2name = N'ModificationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Usuario Modificación', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'MainAccounts', @level2type = N'COLUMN', @level2name = N'ModificationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha de Creación', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'MainAccounts', @level2type = N'COLUMN', @level2name = N'CreationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Usuario Creación', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'MainAccounts', @level2type = N'COLUMN', @level2name = N'CreationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Estado de la cuenta', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'MainAccounts', @level2type = N'COLUMN', @level2name = N'Status';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica que la cuenta se puede utilizar para realizar movimientos, es decir que esta habilitada para estar dentro de un detalle del comprobante contable', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'MainAccounts', @level2type = N'COLUMN', @level2name = N'AllowsMovement';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica si la cuenta contable es de categoria trabajadores independientes,  Este campo solo se habilita si el tipo de retencion es de Tipo Retencion en la Fuente', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'MainAccounts', @level2type = N'COLUMN', @level2name = N'FreelancerCategory';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Tipo de retencion. Ninguna = 0, ReteFuente = 1, ReteIva = 2, ReteIca = 3, Otras = 4', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'MainAccounts', @level2type = N'COLUMN', @level2name = N'RetencionType';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Maneja centro de costo', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'MainAccounts', @level2type = N'COLUMN', @level2name = N'HandlesCostCenter';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica si se muestra en el reporte de CGN02', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'MainAccounts', @level2type = N'COLUMN', @level2name = N'ShowCGN2';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Disponibilidad.   Estas opciones se muestran siempre y cuando la clase de la cuenta sea de tipo balance  Ninguna = 0, Corriente = 1, NoCorriente = 2, Ambas = 3    Estas opciones se muestran siempre y cuando la clase de la cuenta sea de tipo resultado  Operacional = 4, No Operacional = 5    Si son de otro tipo este campo va en cero', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'MainAccounts', @level2type = N'COLUMN', @level2name = N'Availability';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Valor que indica si la cuenta permite conciliar', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'MainAccounts', @level2type = N'COLUMN', @level2name = N'ReconcileAccount';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del tercero', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'MainAccounts', @level2type = N'COLUMN', @level2name = N'IdThirdParty';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Valor que indica si se realiza cierre de terceros', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'MainAccounts', @level2type = N'COLUMN', @level2name = N'CloseThirdParty';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Maneja tercero', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'MainAccounts', @level2type = N'COLUMN', @level2name = N'HandlesThirdParty';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del nodo padre', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'MainAccounts', @level2type = N'COLUMN', @level2name = N'IdParent';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Nombre de la cuenta', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'MainAccounts', @level2type = N'COLUMN', @level2name = N'Name';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Naturaleza contable: 1-Debito, 2-Credito', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'MainAccounts', @level2type = N'COLUMN', @level2name = N'Nature';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Numero de la cuenta', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'MainAccounts', @level2type = N'COLUMN', @level2name = N'Number';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id de la clase de cuenta', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'MainAccounts', @level2type = N'COLUMN', @level2name = N'IdAccountClass';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del nivel de cuenta', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'MainAccounts', @level2type = N'COLUMN', @level2name = N'IdAccountLevel';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica la cuenta contable a que libro de contabilidad pertenece', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'MainAccounts', @level2type = N'COLUMN', @level2name = N'LegalBookId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'ID Autonumerico de la tabla', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'MainAccounts', @level2type = N'COLUMN', @level2name = N'Id';

