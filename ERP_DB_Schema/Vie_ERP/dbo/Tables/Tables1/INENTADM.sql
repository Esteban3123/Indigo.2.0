CREATE TABLE [dbo].[INENTADM] (
    [CODENTADM]     CHAR (9)      NOT NULL,
    [NOMENTADM]     VARCHAR (150) NOT NULL,
    [CODIGONIT]     CHAR (15)     NOT NULL,
    [TIPENTADM]     CHAR (2)      NOT NULL,
    [REGIMEN]       CHAR (1)      NULL,
    [CODSUPERINTEN] VARCHAR (10)  NULL,
    CONSTRAINT [PK_INENTADM] PRIMARY KEY CLUSTERED ([CODENTADM] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Código asignado por superintendencia', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INENTADM', @level2type = N'COLUMN', @level2name = N'CODSUPERINTEN';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Contributivo -> C  Subsidiado ->S  Excepcional->E  Especial->P', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INENTADM', @level2type = N'COLUMN', @level2name = N'REGIMEN';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Tipo Entidad  Si es version Fox  1=EPS;  2=EAS;  3=ARS;  4:=ARP;  5=MP;  6=ESE;  7=EEPS;  8=IPS;  9=EMP;  10=PPN;  11=PPJ;  12=OTRAS  Si es version .NET  0=EPS;  1=EAS;  2=ARS;  3=ARP;  4=MP;  5=ESE;  6=EEPS;  7=IPS;  8=EMP;  9=PPN;  10=PPJ;  11=OTRAS', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INENTADM', @level2type = N'COLUMN', @level2name = N'TIPENTADM';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo Nit', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INENTADM', @level2type = N'COLUMN', @level2name = N'CODIGONIT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Descripcion de la entidad administradora', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INENTADM', @level2type = N'COLUMN', @level2name = N'NOMENTADM';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo Entidad Administradora', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INENTADM', @level2type = N'COLUMN', @level2name = N'CODENTADM';

