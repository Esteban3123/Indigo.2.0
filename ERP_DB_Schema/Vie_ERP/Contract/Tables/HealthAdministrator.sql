CREATE TABLE [Contract].[HealthAdministrator] (
    [Id]               INT           IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [Code]             VARCHAR (20)  NOT NULL,
    [Name]             VARCHAR (300) NOT NULL,
    [ThirdPartyId]     INT           NOT NULL,
    [EntityType]       TINYINT       NOT NULL,
    [HealthEntityCode] VARCHAR (6)   NOT NULL,
    [Status]           BIT           NOT NULL,
    [CreationUser]     VARCHAR (20)  NOT NULL,
    [CreationDate]     DATETIME      NOT NULL,
    [ModificationUser] VARCHAR (20)  NULL,
    [ModificationDate] DATETIME      NULL,
    [TimeStamp]        ROWVERSION    NOT NULL,
    [Regimen4505]      CHAR (1)      NULL,
    [MOSTRARWEB]       BIT           NULL,
    CONSTRAINT [PK_HealthAdministrator__Id] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_HealthAdministrator_ThirdParty] FOREIGN KEY ([ThirdPartyId]) REFERENCES [Common].[ThirdParty] ([Id])
);


GO
ALTER TABLE [Contract].[HealthAdministrator] NOCHECK CONSTRAINT [FK_HealthAdministrator_ThirdParty];


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
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Mostrar citas En Web  1 = Si   0 = No', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'HealthAdministrator', @level2type = N'COLUMN', @level2name = N'MOSTRARWEB';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '"C" para Contributivo  "S" para Subsidiado  "P" para Excepción  "E" para Especial  "N" para No asegurado    Este campo lo pidieron desde Crystal para que lo utilizara Juan David Patiño', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'HealthAdministrator', @level2type = N'COLUMN', @level2name = N'Regimen4505';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Marca temporal en la que ocurrio determinado evento. Guarda el instante tiempo de la creacion , registro o modificacion de un archivo determinado.', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'HealthAdministrator', @level2type = N'COLUMN', @level2name = N'TimeStamp';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha Modificación', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'HealthAdministrator', @level2type = N'COLUMN', @level2name = N'ModificationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Usuario Modificación', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'HealthAdministrator', @level2type = N'COLUMN', @level2name = N'ModificationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha de Creación', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'HealthAdministrator', @level2type = N'COLUMN', @level2name = N'CreationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Usuario Creación', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'HealthAdministrator', @level2type = N'COLUMN', @level2name = N'CreationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Estado del manual tarifario  1 - Activo  0 - Inactivo', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'HealthAdministrator', @level2type = N'COLUMN', @level2name = N'Status';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Código EPS o Entidad Territorial de salud', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'HealthAdministrator', @level2type = N'COLUMN', @level2name = N'HealthEntityCode';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Tipo de entidad  1 - EPS Contributivo  2 - EPS Subsidiado  3 - ET Vinculados Municipios  4 - ET Vinculados Departamentos  5 - ARL Riesgos Laborales  6 - MP Medicina Prepagada  7 - IPS Privada  8 - IPS Publica  9 - Regimen Especial  10 - Accidentes de transito  11 - Fosyga  12 - Otros', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'HealthAdministrator', @level2type = N'COLUMN', @level2name = N'EntityType';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del tercero de la entidad', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'HealthAdministrator', @level2type = N'COLUMN', @level2name = N'ThirdPartyId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Nombre de la entidad', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'HealthAdministrator', @level2type = N'COLUMN', @level2name = N'Name';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo de la entidad', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'HealthAdministrator', @level2type = N'COLUMN', @level2name = N'Code';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id de la entidad administradora de salud', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'HealthAdministrator', @level2type = N'COLUMN', @level2name = N'Id';

