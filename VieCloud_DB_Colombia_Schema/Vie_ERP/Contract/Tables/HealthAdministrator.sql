CREATE TABLE [Contract].[HealthAdministrator] (
    [Id]                                     INT           IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [Code]                                   VARCHAR (20)  NOT NULL,
    [Name]                                   VARCHAR (300) NOT NULL,
    [ThirdPartyId]                           INT           NOT NULL,
    [EntityType]                             TINYINT       NOT NULL,
    [HealthEntityCode]                       VARCHAR (6)   NOT NULL,
    [Status]                                 BIT           NOT NULL,
    [CreationUser]                           VARCHAR (20)  NOT NULL,
    [CreationDate]                           DATETIME      NOT NULL,
    [ModificationUser]                       VARCHAR (20)  NULL,
    [ModificationDate]                       DATETIME      NULL,
    [TimeStamp]                              ROWVERSION    NOT NULL,
    [Regimen4505]                            CHAR (1)      NULL,
    [MOSTRARWEB]                             BIT           NULL,
    [PortfolioDeteriorationClassificationId] INT           NULL,
    CONSTRAINT [PK_HealthAdministrator__Id] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_HealthAdministrator_PortfolioDeterioration] FOREIGN KEY ([PortfolioDeteriorationClassificationId]) REFERENCES [Portfolio].[PortfolioDeteriorationClassification] ([Id]),
    CONSTRAINT [FK_HealthAdministrator_ThirdParty] FOREIGN KEY ([ThirdPartyId]) REFERENCES [Common].[ThirdParty] ([Id])
);




GO





GO



GO
CREATE UNIQUE NONCLUSTERED INDEX [UQ_HealthAdministrator__Code]
    ON [Contract].[HealthAdministrator]([Code] ASC);


GO
CREATE TRIGGER [Contract].[TriggerHealthAdministratorVieCrystal]
   ON  [Contract].[HealthAdministrator]
   AFTER INSERT, UPDATE
AS 

-- UPDATE
IF EXISTS (SELECT * FROM inserted) AND EXISTS (SELECT * FROM deleted)
BEGIN

  	UPDATE [dbo].[INENTIDAD]
            SET CODENTIDA = (select Code from inserted)
           ,NOMENTIDA = (select Name from inserted)
           ,CODIGONIT = (select RIGHT('000000000000000' + Ltrim(Rtrim((select Nit from Common.ThirdParty where Id = ThirdPartyId))),15) from inserted)
           ,CODADMPAG = ''
           ,DIGITOVER = 0
           ,ENTDIRECC = ''
           ,ENTITELEF = ''
           ,ENTCONTAC = (select Name from inserted)
           ,ENTIEMAIL = ''
           ,INDAUDFOR = 0
			WHERE CODENTIDA = (select Code from inserted)
END

-- INSERT
IF EXISTS (SELECT * FROM inserted) AND NOT EXISTS(SELECT * FROM deleted)
BEGIN
  	INSERT INTO [dbo].[INENTIDAD]
           ([CODENTIDA]
           ,[NOMENTIDA]
           ,[CODIGONIT]
           ,[CODADMPAG]
           ,[DIGITOVER]
           ,[ENTDIRECC]
           ,[ENTITELEF]
           ,[ENTCONTAC]
           ,[ENTIEMAIL]
           ,[INDAUDFOR])
	select Code, Name, RIGHT('000000000000000' + Ltrim(Rtrim((select Nit from Common.ThirdParty where Id = ThirdPartyId))),15),'',0,'','',Name,'',0  from inserted

END
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador booleano (BIT) que controla la visibilidad de citas en el portal web: 1=Sí mostrar, 0=No mostrar. Afecta la disponibilidad de agendamiento online para pacientes.', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'HealthAdministrator', @level2type = N'COLUMN', @level2name = N'MOSTRARWEB';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Mostrar citas En Web  1 = Si   0 = No', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'HealthAdministrator', @level2type = N'COLUMN', @level2name = N'MOSTRARWEB';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'HealthAdministrator', @level2type = N'COLUMN', @level2name = N'MOSTRARWEB';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Clasificación del régimen de afiliación (CHAR): ''''C''''=Contributivo, ''''S''''=Subsidiado, ''''P''''=Excepción, ''''E''''=Especial, ''''N''''=No asegurado. Campo requerido para reportes Crystal y análisis de cobertura.', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'HealthAdministrator', @level2type = N'COLUMN', @level2name = N'Regimen4505';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'"C" para Contributivo  "S" para Subsidiado  "P" para Excepción  "E" para Especial  "N" para No asegurado    Este campo lo pidieron desde Crystal para que lo utilizara Juan David Patiño', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'HealthAdministrator', @level2type = N'COLUMN', @level2name = N'Regimen4505';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'HealthAdministrator', @level2type = N'COLUMN', @level2name = N'Regimen4505';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Marca de tiempo (TIMESTAMP) que registra automáticamente el instante exacto de creación, modificación o registro del administrador de salud. Auditoria temporal de cambios.', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'HealthAdministrator', @level2type = N'COLUMN', @level2name = N'TimeStamp';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Marca temporal en la que ocurrio determinado evento. Guarda el instante tiempo de la creacion , registro o modificacion de un archivo determinado.', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'HealthAdministrator', @level2type = N'COLUMN', @level2name = N'TimeStamp';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'HealthAdministrator', @level2type = N'COLUMN', @level2name = N'TimeStamp';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora (DATETIME, nullable) de la última modificación del registro del administrador de salud. NULL si nunca fue modificado.', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'HealthAdministrator', @level2type = N'COLUMN', @level2name = N'ModificationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha Modificación', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'HealthAdministrator', @level2type = N'COLUMN', @level2name = N'ModificationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'HealthAdministrator', @level2type = N'COLUMN', @level2name = N'ModificationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador (VARCHAR 20, nullable) del usuario que realizó la última modificación del registro. NULL si el registro nunca fue editado.', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'HealthAdministrator', @level2type = N'COLUMN', @level2name = N'ModificationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Usuario Modificación', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'HealthAdministrator', @level2type = N'COLUMN', @level2name = N'ModificationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'HealthAdministrator', @level2type = N'COLUMN', @level2name = N'ModificationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora (DATETIME) de creación del registro del administrador de salud. Marca la generación inicial del contrato o entidad.', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'HealthAdministrator', @level2type = N'COLUMN', @level2name = N'CreationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha de Creación', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'HealthAdministrator', @level2type = N'COLUMN', @level2name = N'CreationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'HealthAdministrator', @level2type = N'COLUMN', @level2name = N'CreationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador (VARCHAR 20) del usuario responsable de la creación inicial del registro del administrador de salud.', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'HealthAdministrator', @level2type = N'COLUMN', @level2name = N'CreationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Usuario Creación', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'HealthAdministrator', @level2type = N'COLUMN', @level2name = N'CreationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'HealthAdministrator', @level2type = N'COLUMN', @level2name = N'CreationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Estado activo/inactivo (BIT) del administrador de salud: 1=Activo, 0=Inactivo. Controla si la entidad está habilitada para operaciones.', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'HealthAdministrator', @level2type = N'COLUMN', @level2name = N'Status';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Estado del manual tarifario  1 - Activo  0 - Inactivo', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'HealthAdministrator', @level2type = N'COLUMN', @level2name = N'Status';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'HealthAdministrator', @level2type = N'COLUMN', @level2name = N'Status';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código de identificación (VARCHAR 6) de la entidad de salud: EPS (Empresa Promotora de Salud) o Entidad Territorial de salud. Identificador RIPS.', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'HealthAdministrator', @level2type = N'COLUMN', @level2name = N'HealthEntityCode';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Código EPS o Entidad Territorial de salud', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'HealthAdministrator', @level2type = N'COLUMN', @level2name = N'HealthEntityCode';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'HealthAdministrator', @level2type = N'COLUMN', @level2name = N'HealthEntityCode';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tipo de entidad de salud (TINYINT 1-12): EPS Contributivo/Subsidiado, Entidades Territoriales, ARL, Medicina Prepagada, IPS Privada/Pública, Régimen Especial, Accidentes de tránsito, Fosyga, Otros.', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'HealthAdministrator', @level2type = N'COLUMN', @level2name = N'EntityType';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Tipo de entidad  1 - EPS Contributivo  2 - EPS Subsidiado  3 - ET Vinculados Municipios  4 - ET Vinculados Departamentos  5 - ARL Riesgos Laborales  6 - MP Medicina Prepagada  7 - IPS Privada  8 - IPS Publica  9 - Regimen Especial  10 - Accidentes de transito  11 - Fosyga  12 - Otros', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'HealthAdministrator', @level2type = N'COLUMN', @level2name = N'EntityType';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'HealthAdministrator', @level2type = N'COLUMN', @level2name = N'EntityType';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Clave foránea (INT, FK) que referencia el tercero/entidad en [Common].[ThirdParty]. Vincula el administrador de salud con datos maestros del tercero.', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'HealthAdministrator', @level2type = N'COLUMN', @level2name = N'ThirdPartyId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del tercero de la entidad', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'HealthAdministrator', @level2type = N'COLUMN', @level2name = N'ThirdPartyId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'HealthAdministrator', @level2type = N'COLUMN', @level2name = N'ThirdPartyId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Nombre o razón social (VARCHAR 300) del administrador de salud, EPS, entidad territorial o prestador. Identificable públicamente.', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'HealthAdministrator', @level2type = N'COLUMN', @level2name = N'Name';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Nombre de la entidad', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'HealthAdministrator', @level2type = N'COLUMN', @level2name = N'Name';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'HealthAdministrator', @level2type = N'COLUMN', @level2name = N'Name';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código único (VARCHAR 20) de la entidad administradora de salud. Identificador corto para búsquedas y reportes de contrato.', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'HealthAdministrator', @level2type = N'COLUMN', @level2name = N'Code';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo de la entidad', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'HealthAdministrator', @level2type = N'COLUMN', @level2name = N'Code';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'HealthAdministrator', @level2type = N'COLUMN', @level2name = N'Code';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único (INT, PK) del administrador de salud. Clave primaria para todas las referencias internas al contrato sanitario.', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'HealthAdministrator', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de la entidad administradora de salud', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'HealthAdministrator', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'HealthAdministrator', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Clave foránea (INT, nullable, FK) hacia [Portfolio].[PortfolioDeteriorationClassification]. Clasifica el nivel de deterioro o riesgo de cartera del administrador.', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'HealthAdministrator', @level2type = N'COLUMN', @level2name = N'PortfolioDeteriorationClassificationId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Clasificación de deterioro la Cartera.', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'HealthAdministrator', @level2type = N'COLUMN', @level2name = N'PortfolioDeteriorationClassificationId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'HealthAdministrator', @level2type = N'COLUMN', @level2name = N'PortfolioDeteriorationClassificationId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Administradoras de salud (EPS, ARS, aseguradoras, pagadores) con las que la institución tiene contratos vigentes o históricos. Incluye datos de identificación, tipo de entidad, régimen y estado de cada pagador.', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'HealthAdministrator';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'HealthAdministrator';
