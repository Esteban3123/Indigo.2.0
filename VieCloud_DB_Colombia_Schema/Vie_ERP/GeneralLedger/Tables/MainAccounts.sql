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
ALTER TABLE [GeneralLedger].[MainAccounts] NOCHECK CONSTRAINT [FK_MainAccounts_LegalBook];


GO



GO



GO



GO



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
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Bandera BIT (0/1) que indica restricción de centro de costos: 0=sin restricciones, 1=restringe todos excepto los definidos en MainAccountRestrictions. Controla filtrado y disponibilidad de centros de costo en movimientos contables.', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'MainAccounts', @level2type = N'COLUMN', @level2name = N'HandlesCostCenterRestriction';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'   0: no tiene restricciones de centro de costos   1: Restringe todos los centros de costos excepto los agregados en la tabla [GeneralLedger].[MainAccountRestrictions]   ', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'MainAccounts', @level2type = N'COLUMN', @level2name = N'HandlesCostCenterRestriction';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'MainAccounts', @level2type = N'COLUMN', @level2name = N'HandlesCostCenterRestriction';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Bandera BIT (0/1) que indica restricción de terceros: 0=sin restricciones, 1=restringe todos los terceros habilitando solo los agregados en MainAccountRestrictions. Controla acceso y filtrado de terceros (proveedores, clientes, entidades).', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'MainAccounts', @level2type = N'COLUMN', @level2name = N'HandlesThirdPartyRestriction';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'   0: no tiene restricciones de terceros   1: Restringe todos los terceros y habilita solo los agregados en la tabla [GeneralLedger].[MainAccountRestrictions]   ', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'MainAccounts', @level2type = N'COLUMN', @level2name = N'HandlesThirdPartyRestriction';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'MainAccounts', @level2type = N'COLUMN', @level2name = N'HandlesThirdPartyRestriction';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Bandera BIT (0/1) que indica si la cuenta maneja base de cálculo. Habilita procesamiento de base contable en retenciones y cálculos de impuestos.', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'MainAccounts', @level2type = N'COLUMN', @level2name = N'HandleBase';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Maneja Base', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'MainAccounts', @level2type = N'COLUMN', @level2name = N'HandleBase';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'MainAccounts', @level2type = N'COLUMN', @level2name = N'HandleBase';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Marca temporal TIMESTAMP SQL Server: captura automática del instante exacto de creación, registro o modificación de la cuenta. Auditoría de eventos contables.', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'MainAccounts', @level2type = N'COLUMN', @level2name = N'TimeStamp';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Marca temporal en la que ocurrio determinado evento. Guarda el instante tiempo de la creacion , registro o modificacion de un archivo determinado.', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'MainAccounts', @level2type = N'COLUMN', @level2name = N'TimeStamp';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'MainAccounts', @level2type = N'COLUMN', @level2name = N'TimeStamp';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha DATETIME de última modificación de la cuenta. Trazabilidad de cambios en configuración contable.', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'MainAccounts', @level2type = N'COLUMN', @level2name = N'ModificationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha Modificación', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'MainAccounts', @level2type = N'COLUMN', @level2name = N'ModificationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'MainAccounts', @level2type = N'COLUMN', @level2name = N'ModificationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Usuario VARCHAR(20) que realizó la última modificación. Auditoría de quién cambió la cuenta.', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'MainAccounts', @level2type = N'COLUMN', @level2name = N'ModificationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Usuario Modificación', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'MainAccounts', @level2type = N'COLUMN', @level2name = N'ModificationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'MainAccounts', @level2type = N'COLUMN', @level2name = N'ModificationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha DATETIME de creación inicial de la cuenta. Trazabilidad del alta en el sistema contable.', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'MainAccounts', @level2type = N'COLUMN', @level2name = N'CreationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha de Creación', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'MainAccounts', @level2type = N'COLUMN', @level2name = N'CreationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'MainAccounts', @level2type = N'COLUMN', @level2name = N'CreationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Usuario VARCHAR(20) que creó la cuenta. Auditoría de origen en el sistema.', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'MainAccounts', @level2type = N'COLUMN', @level2name = N'CreationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Usuario Creación', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'MainAccounts', @level2type = N'COLUMN', @level2name = N'CreationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'MainAccounts', @level2type = N'COLUMN', @level2name = N'CreationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Bandera BIT activa/inactiva (default=1): indica si la cuenta está habilitada (1) o desactivada (0) para movimientos contables.', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'MainAccounts', @level2type = N'COLUMN', @level2name = N'Status';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Estado de la cuenta', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'MainAccounts', @level2type = N'COLUMN', @level2name = N'Status';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'MainAccounts', @level2type = N'COLUMN', @level2name = N'Status';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Bandera BIT (0/1) que especifica si la cuenta está habilitada para registrar movimientos en detalles de comprobantes contables. Controla viabilidad de uso operativo.', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'MainAccounts', @level2type = N'COLUMN', @level2name = N'AllowsMovement';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica que la cuenta se puede utilizar para realizar movimientos, es decir que esta habilitada para estar dentro de un detalle del comprobante contable', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'MainAccounts', @level2type = N'COLUMN', @level2name = N'AllowsMovement';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'MainAccounts', @level2type = N'COLUMN', @level2name = N'AllowsMovement';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Bandera BIT (0/1) que clasifica cuenta como categoría de trabajadores independientes. Se habilita solo si RetencionType es ReteFuente (1).', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'MainAccounts', @level2type = N'COLUMN', @level2name = N'FreelancerCategory';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica si la cuenta contable es de categoria trabajadores independientes,  Este campo solo se habilita si el tipo de retencion es de Tipo Retencion en la Fuente', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'MainAccounts', @level2type = N'COLUMN', @level2name = N'FreelancerCategory';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'MainAccounts', @level2type = N'COLUMN', @level2name = N'FreelancerCategory';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'TINYINT: tipo de retención aplicable. 0=Ninguna, 1=ReteFuente, 2=ReteIva, 3=ReteIca, 4=Otras. Define obligaciones tributarias.', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'MainAccounts', @level2type = N'COLUMN', @level2name = N'RetencionType';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Tipo de retencion. Ninguna = 0, ReteFuente = 1, ReteIva = 2, ReteIca = 3, Otras = 4', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'MainAccounts', @level2type = N'COLUMN', @level2name = N'RetencionType';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'MainAccounts', @level2type = N'COLUMN', @level2name = N'RetencionType';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Bandera BIT (0/1) que indica si la cuenta maneja/requiere asignación de centro de costo. Habilita control de distribución de gastos.', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'MainAccounts', @level2type = N'COLUMN', @level2name = N'HandlesCostCenter';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Maneja centro de costo', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'MainAccounts', @level2type = N'COLUMN', @level2name = N'HandlesCostCenter';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'MainAccounts', @level2type = N'COLUMN', @level2name = N'HandlesCostCenter';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Bandera BIT (default=1) que especifica si la cuenta aparece en reporte CGN02. Controla visibilidad en reportes tributarios.', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'MainAccounts', @level2type = N'COLUMN', @level2name = N'ShowCGN2';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica si se muestra en el reporte de CGN02', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'MainAccounts', @level2type = N'COLUMN', @level2name = N'ShowCGN2';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'MainAccounts', @level2type = N'COLUMN', @level2name = N'ShowCGN2';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'TINYINT: disponibilidad/clasificación según clase de cuenta. Balance: 0=Ninguna, 1=Corriente, 2=NoCorriente, 3=Ambas. Resultado: 4=Operacional, 5=No Operacional. 0=Otros.', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'MainAccounts', @level2type = N'COLUMN', @level2name = N'Availability';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Disponibilidad.   Estas opciones se muestran siempre y cuando la clase de la cuenta sea de tipo balance  Ninguna = 0, Corriente = 1, NoCorriente = 2, Ambas = 3    Estas opciones se muestran siempre y cuando la clase de la cuenta sea de tipo resultado  Operacional = 4, No Operacional = 5    Si son de otro tipo este campo va en cero', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'MainAccounts', @level2type = N'COLUMN', @level2name = N'Availability';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'MainAccounts', @level2type = N'COLUMN', @level2name = N'Availability';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Bandera BIT (0/1) que indica si la cuenta permite conciliación. Habilita procesos de reconciliación bancaria o contable.', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'MainAccounts', @level2type = N'COLUMN', @level2name = N'ReconcileAccount';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Valor que indica si la cuenta permite conciliar', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'MainAccounts', @level2type = N'COLUMN', @level2name = N'ReconcileAccount';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'MainAccounts', @level2type = N'COLUMN', @level2name = N'ReconcileAccount';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'INT: identificador (FK) del tercero asociado (proveedor, cliente, entidad). Referencia a Common.ThirdParty.', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'MainAccounts', @level2type = N'COLUMN', @level2name = N'IdThirdParty';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del tercero', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'MainAccounts', @level2type = N'COLUMN', @level2name = N'IdThirdParty';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'MainAccounts', @level2type = N'COLUMN', @level2name = N'IdThirdParty';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Bandera BIT (0/1) que indica si se aplica cierre de terceros. Controla finalización de relaciones con terceros.', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'MainAccounts', @level2type = N'COLUMN', @level2name = N'CloseThirdParty';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Valor que indica si se realiza cierre de terceros', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'MainAccounts', @level2type = N'COLUMN', @level2name = N'CloseThirdParty';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'MainAccounts', @level2type = N'COLUMN', @level2name = N'CloseThirdParty';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Bandera BIT (0/1) que indica si la cuenta maneja/requiere asociación a tercero (proveedor, cliente, acreedor).', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'MainAccounts', @level2type = N'COLUMN', @level2name = N'HandlesThirdParty';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Maneja tercero', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'MainAccounts', @level2type = N'COLUMN', @level2name = N'HandlesThirdParty';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'MainAccounts', @level2type = N'COLUMN', @level2name = N'HandlesThirdParty';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'INT: identificador (FK) del nodo padre en jerarquía de cuentas. Autorrelación para estructura de plan de cuentas.', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'MainAccounts', @level2type = N'COLUMN', @level2name = N'IdParent';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del nodo padre', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'MainAccounts', @level2type = N'COLUMN', @level2name = N'IdParent';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'MainAccounts', @level2type = N'COLUMN', @level2name = N'IdParent';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'VARCHAR(300): nombre descriptivo de la cuenta contable. Ej: Bancos, Cuentas por cobrar, Ingresos operacionales.', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'MainAccounts', @level2type = N'COLUMN', @level2name = N'Name';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Nombre de la cuenta', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'MainAccounts', @level2type = N'COLUMN', @level2name = N'Name';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'MainAccounts', @level2type = N'COLUMN', @level2name = N'Name';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'TINYINT: naturaleza contable. 1=Débito (activo/gasto), 2=Crédito (pasivo/ingreso). Define comportamiento en registros.', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'MainAccounts', @level2type = N'COLUMN', @level2name = N'Nature';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Naturaleza contable: 1-Debito, 2-Credito', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'MainAccounts', @level2type = N'COLUMN', @level2name = N'Nature';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'MainAccounts', @level2type = N'COLUMN', @level2name = N'Nature';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'VARCHAR(50): código/número único de la cuenta según plan de cuentas. Ej: 1105, 2205. Identificador de búsqueda.', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'MainAccounts', @level2type = N'COLUMN', @level2name = N'Number';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Numero de la cuenta', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'MainAccounts', @level2type = N'COLUMN', @level2name = N'Number';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'MainAccounts', @level2type = N'COLUMN', @level2name = N'Number';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'INT: identificador (FK) de la clase de cuenta. Referencia MainAccountClasses (Balance, Resultado, etc).', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'MainAccounts', @level2type = N'COLUMN', @level2name = N'IdAccountClass';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de la clase de cuenta', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'MainAccounts', @level2type = N'COLUMN', @level2name = N'IdAccountClass';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'MainAccounts', @level2type = N'COLUMN', @level2name = N'IdAccountClass';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'INT: identificador (FK) del nivel de cuenta en jerarquía. Referencia MainAccountLevels (nivel 1, 2, 3, etc).', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'MainAccounts', @level2type = N'COLUMN', @level2name = N'IdAccountLevel';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del nivel de cuenta', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'MainAccounts', @level2type = N'COLUMN', @level2name = N'IdAccountLevel';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'MainAccounts', @level2type = N'COLUMN', @level2name = N'IdAccountLevel';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'INT: identificador (FK) del libro de contabilidad asignado. Referencia LegalBook. Especifica separación legal/fiscal.', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'MainAccounts', @level2type = N'COLUMN', @level2name = N'LegalBookId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica la cuenta contable a que libro de contabilidad pertenece', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'MainAccounts', @level2type = N'COLUMN', @level2name = N'LegalBookId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'MainAccounts', @level2type = N'COLUMN', @level2name = N'LegalBookId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'INT IDENTITY(1,1): identificador autonumérico único de la cuenta principal. Clave primaria de MainAccounts.', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'MainAccounts', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'ID Autonumerico de la tabla', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'MainAccounts', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'MainAccounts', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Plan de cuentas contables del libro mayor. Contiene cada cuenta contable (PUC) con su jerarquía, naturaleza, configuración de terceros, centros de costo y parámetros de comportamiento para el registro de movimientos contables y retenciones.', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'MainAccounts';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'MainAccounts';
