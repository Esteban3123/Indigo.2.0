CREATE TABLE [dbo].[HCPERFILFD] (
    [IDPERFILFD]     INT            IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [IDPERFIL]       INT            NOT NULL,
    [FECHA]          DATETIME       NOT NULL,
    [CODPRODUC]      CHAR (20)      NOT NULL,
    [UNITDOSETYPE]   VARCHAR (3)    NOT NULL,
    [ADMINISTRATION] VARCHAR (50)   NOT NULL,
    [INTERVAL]       VARCHAR (15)   NOT NULL,
    [ESTATUS]        INT            NOT NULL,
    [ALERTAS]        VARCHAR (150)  NULL,
    [OBSERVATION]    VARCHAR (4000) NULL,
    CONSTRAINT [PK__HCPERFIL__959B7444699CE337] PRIMARY KEY CLUSTERED ([IDPERFILFD] ASC),
    CONSTRAINT [fk_PerfilFarmaco] FOREIGN KEY ([IDPERFIL]) REFERENCES [dbo].[HCPFARMACOC] ([IDPERFIL])
);


GO
ALTER TABLE [dbo].[HCPERFILFD] NOCHECK CONSTRAINT [fk_PerfilFarmaco];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda la observación', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPERFILFD', @level2type = N'COLUMN', @level2name = N'OBSERVATION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda las alertas ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPERFILFD', @level2type = N'COLUMN', @level2name = N'ALERTAS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda el estado ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPERFILFD', @level2type = N'COLUMN', @level2name = N'ESTATUS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda la aministracción', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPERFILFD', @level2type = N'COLUMN', @level2name = N'ADMINISTRATION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda el tipo de dosis unitaria', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPERFILFD', @level2type = N'COLUMN', @level2name = N'UNITDOSETYPE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda el Codigo Del Producto', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPERFILFD', @level2type = N'COLUMN', @level2name = N'CODPRODUC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda la Fecha de registro', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPERFILFD', @level2type = N'COLUMN', @level2name = N'FECHA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda el Id del Perfil', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPERFILFD', @level2type = N'COLUMN', @level2name = N'IDPERFIL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda el consecutivo de la tabla ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPERFILFD', @level2type = N'COLUMN', @level2name = N'IDPERFILFD';

