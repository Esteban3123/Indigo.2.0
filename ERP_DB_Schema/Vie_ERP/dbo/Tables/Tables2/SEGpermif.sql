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


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Grilla Eliminar Registro  1-> Si', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SEGpermif', @level2type = N'COLUMN', @level2name = N'igrielimi';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Grilla Modificar Registro 1-> Si', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SEGpermif', @level2type = N'COLUMN', @level2name = N'igrimodif';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Grilla Crear Registro 1-> Si', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SEGpermif', @level2type = N'COLUMN', @level2name = N'igricrear';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Permite Imprimir 1-> Si', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SEGpermif', @level2type = N'COLUMN', @level2name = N'ioptimpri';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Permite Anular 1-> Si', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SEGpermif', @level2type = N'COLUMN', @level2name = N'ioptanula';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Permite Confirmar 1-> Si', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SEGpermif', @level2type = N'COLUMN', @level2name = N'ioptconfi';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Formulario Navegar  1-> Si', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SEGpermif', @level2type = N'COLUMN', @level2name = N'ioptnaveg';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Formulario eliminar  1-> Si', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SEGpermif', @level2type = N'COLUMN', @level2name = N'ioptelimi';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Formulario Actualizar 1-> Si', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SEGpermif', @level2type = N'COLUMN', @level2name = N'ioptactua';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Formulario Guardar Como Diseñar (Guardar Como) 1-> Si', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SEGpermif', @level2type = N'COLUMN', @level2name = N'ioptdisen';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Formulario Consultar 1-> Si', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SEGpermif', @level2type = N'COLUMN', @level2name = N'ioptconsu';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Formulario Guardar 1-> Si', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SEGpermif', @level2type = N'COLUMN', @level2name = N'ioptguard';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Descripcion del Menu', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SEGpermif', @level2type = N'COLUMN', @level2name = N'indmendes';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo del Menu en INDIGO', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SEGpermif', @level2type = N'COLUMN', @level2name = N'indidmenu';

