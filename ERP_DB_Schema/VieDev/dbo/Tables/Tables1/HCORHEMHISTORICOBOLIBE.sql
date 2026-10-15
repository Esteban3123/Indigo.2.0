CREATE TABLE [dbo].[HCORHEMHISTORICOBOLIBE] (
    [ID]           INT            IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [IDHCORHEMBOL] INT            NOT NULL,
    [HCORHEMCOID]  INT            NOT NULL,
    [COMSAMID]     INT            NOT NULL,
    [FECRESERVA]   DATETIME       NOT NULL,
    [BACRESPRES]   CHAR (20)      NOT NULL,
    [REAPRUECRU]   BIT            NOT NULL,
    [NUMBOLSA]     VARCHAR (20)   NOT NULL,
    [SELLOCALIDAD] VARCHAR (20)   NOT NULL,
    [GRUPOBOL]     TINYINT        NOT NULL,
    [FECHAEXPIRA]  DATETIME       NOT NULL,
    [PROFLIBERO]   CHAR (20)      NOT NULL,
    [MOTLIBRESID]  INT            NOT NULL,
    [MOTLIBRESDES] VARCHAR (5000) NOT NULL,
    [ASPECFISICO]  TINYINT        NULL,
    [TEMPENTREG]   INT            NULL,
    [AUTENTREGA]   BIT            NULL,
    CONSTRAINT [PK_HCORHEMHISTORICOBOLIBE] PRIMARY KEY CLUSTERED ([ID] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica si autoriza entrega:   True=Si   False=No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORHEMHISTORICOBOLIBE', @level2type = N'COLUMN', @level2name = N'AUTENTREGA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Aspecto fisico de la unidad (bolsa):   1=Adecuado   2=Inadecuado', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORHEMHISTORICOBOLIBE', @level2type = N'COLUMN', @level2name = N'ASPECFISICO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Grupo de bolsa:  Grupo de bolsa:  1 - A+  2 - A-  3 - B+  4 - B-  5 - AB+  6 - AB-  7 - O+  8 - O-', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORHEMHISTORICOBOLIBE', @level2type = N'COLUMN', @level2name = N'GRUPOBOL';

