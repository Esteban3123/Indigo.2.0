CREATE TABLE [dbo].[SEGpermif] (
    [indidmenu]     VARCHAR (5) NOT NULL,
    [indmendes]     CHAR (80)   NOT NULL,
    [ioptguard]     BIT         NOT NULL,
    [ioptconsu]     BIT         NOT NULL,
    [ioptdisen]     BIT         NOT NULL,
    [ioptactua]     BIT         NOT NULL,
    [ioptelimi]     BIT         NOT NULL,
    [ioptnaveg]     BIT         NOT NULL,
    [ioptconfi]     BIT         NOT NULL,
    [ioptanula]     BIT         NOT NULL,
    [ioptimpri]     BIT         NOT NULL,
    [igricrear]     BIT         NOT NULL,
    [igrimodif]     BIT         NOT NULL,
    [igrielimi]     BIT         NOT NULL,
    [integracion]   TINYINT     CONSTRAINT [DF_SEGpermif_integracion] DEFAULT ((1)) NOT NULL,
    [esfundacional] BIT         NULL,
    CONSTRAINT [PK_SEGpermif] PRIMARY KEY CLUSTERED ([indidmenu] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Indica si el formulario es fundacional o no.  0 - No fundacional (guarda en la BD transaccional)  1 - Fundacional (guarda en una BD fundacional y replica)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SEGpermif', @level2type = N'COLUMN', @level2name = N'esfundacional';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '1 - Integracion con DGH y con VIE   -  2 - Integracion solo con VIE    -  3 - Integracion solo con DGH   -', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SEGpermif', @level2type = N'COLUMN', @level2name = N'integracion';

