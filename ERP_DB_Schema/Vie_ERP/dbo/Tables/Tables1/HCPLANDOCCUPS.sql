CREATE TABLE [dbo].[HCPLANDOCCUPS] (
    [ID]          INT          IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [IDHCPLANDOC] CHAR (6)     NOT NULL,
    [CODSERIPS]   CHAR (20)    NOT NULL,
    [CODCOMSAM]   VARCHAR (20) NULL,
    CONSTRAINT [PK_HCPLANDOCCUPS] PRIMARY KEY CLUSTERED ([ID] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Codigo del componente sanguineo, unicamente para los procedimientos de tipo Hemocomponentes ( 9 en la tabla INCUPSIPS) relaciona con el CODCOMSAM de la tabla HCCOMSAN', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPLANDOCCUPS', @level2type = N'COLUMN', @level2name = N'CODCOMSAM';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Relacion con la tabla de Servicios IPS del EHR que es  INCUPSIPS  por medio del campo CODSERIPS  ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPLANDOCCUPS', @level2type = N'COLUMN', @level2name = N'CODSERIPS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Relacion con la tabla de plantillas y documentos (HCPLANDOC)    Relacion por medio del campo (CODCONSEC)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPLANDOCCUPS', @level2type = N'COLUMN', @level2name = N'IDHCPLANDOC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id de la tabla, campo autoincrementable ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPLANDOCCUPS', @level2type = N'COLUMN', @level2name = N'ID';

