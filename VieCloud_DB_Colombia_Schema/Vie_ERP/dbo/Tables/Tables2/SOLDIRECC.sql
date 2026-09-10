CREATE TABLE [dbo].[SOLDIRECC] (
    [DIREAUTO]       TINYINT      IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [Direccion]      VARCHAR (80) NOT NULL,
    [PROVAUTO]       NUMERIC (18) NOT NULL,
    [DirecPrincipal] BIT          NOT NULL,
    CONSTRAINT [PK_SOLDIREC] PRIMARY KEY CLUSTERED ([DIREAUTO] ASC),
    CONSTRAINT [FK_SOLDIREC_SOLPROVE] FOREIGN KEY ([PROVAUTO]) REFERENCES [dbo].[SOLPROVEE] ([PROVAUTO])
);




GO



GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador booleano (BIT) que señala si esta es la dirección principal o sede del proveedor. Valores: 1=Dirección principal, 0=Dirección secundaria/alternativa.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SOLDIRECC', @level2type = N'COLUMN', @level2name = N'DirecPrincipal';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Contiene si la direccion es direccion principal', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SOLDIRECC', @level2type = N'COLUMN', @level2name = N'DirecPrincipal';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SOLDIRECC', @level2type = N'COLUMN', @level2name = N'DirecPrincipal';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador numérico del proveedor (FK a SOLPROVEE.PROVAUTO). Vincula cada dirección con su proveedor correspondiente en el ERP.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SOLDIRECC', @level2type = N'COLUMN', @level2name = N'PROVAUTO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Contiene le autonumerico del proveedor', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SOLDIRECC', @level2type = N'COLUMN', @level2name = N'PROVAUTO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SOLDIRECC', @level2type = N'COLUMN', @level2name = N'PROVAUTO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Domicilio o ubicación física del proveedor (VARCHAR 80). Puede incluir calle, número, apartamento, barrio, ciudad.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SOLDIRECC', @level2type = N'COLUMN', @level2name = N'Direccion';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Contiene la direccion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SOLDIRECC', @level2type = N'COLUMN', @level2name = N'Direccion';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SOLDIRECC', @level2type = N'COLUMN', @level2name = N'Direccion';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único autoincrementable (TINYINT) de la dirección del proveedor. Clave primaria de la tabla SOLDIRECC.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SOLDIRECC', @level2type = N'COLUMN', @level2name = N'DIREAUTO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Contiene el autonumerico de la tabla', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SOLDIRECC', @level2type = N'COLUMN', @level2name = N'DIREAUTO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SOLDIRECC', @level2type = N'COLUMN', @level2name = N'DIREAUTO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Catálogo de direcciones disponibles para asociar a pacientes, personas o entidades. Registra las direcciones físicas con su provincia y si es la dirección principal.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SOLDIRECC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SOLDIRECC';
