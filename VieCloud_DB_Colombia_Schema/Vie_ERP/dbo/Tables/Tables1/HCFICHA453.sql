CREATE TABLE [dbo].[HCFICHA453] (
    [ID]                  INT           IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [IDFICHANOTIFICACION] INT           NOT NULL,
    [CODDIAGNO]           CHAR (4)      NULL,
    [LESIOCAUSAEXT]       BIT           NULL,
    [ASFIXIA]             BIT           NULL,
    [ESTRANGULA]          BIT           NULL,
    [HERIDA]              BIT           NULL,
    [TRAUMALEV]           BIT           NULL,
    [CHOQUEELEC]          BIT           NULL,
    [FRACTURA]            BIT           NULL,
    [POLITRAUMA]          BIT           NULL,
    [AMPUTACION]          BIT           NULL,
    [QUEMADURAS]          BIT           NULL,
    [INTOXICACION]        BIT           NULL,
    [INFECCION]           BIT           NULL,
    [SEPSIS]              BIT           NULL,
    [PERFORACION]         BIT           NULL,
    [HEMORRAGIA]          BIT           NULL,
    [NECROSIS]            BIT           NULL,
    [EMBOLIA]             BIT           NULL,
    [DEPRESION]           BIT           NULL,
    [CRANEO]              BIT           NULL,
    [MANO]                BIT           NULL,
    [MUSLOS]              BIT           NULL,
    [CARA]                BIT           NULL,
    [DEDOSMANO]           BIT           NULL,
    [PIERNAS]             BIT           NULL,
    [OJOS]                BIT           NULL,
    [TORAXANTE]           BIT           NULL,
    [PIES]                BIT           NULL,
    [NARIZ]               BIT           NULL,
    [TORAXPOST]           BIT           NULL,
    [DEDOSPIES]           BIT           NULL,
    [OREJAS]              BIT           NULL,
    [MAMAS]               BIT           NULL,
    [ORGANINTER]          BIT           NULL,
    [BOCADIENTES]         BIT           NULL,
    [ABDOMEN]             BIT           NULL,
    [PIEL]                BIT           NULL,
    [CUELLO]              BIT           NULL,
    [PELVISPERI]          BIT           NULL,
    [BRAZO]               BIT           NULL,
    [GENITALES]           BIT           NULL,
    [ANTEBRAZO]           BIT           NULL,
    [GLUTEOS]             BIT           NULL,
    [MAQUINA]             BIT           NULL,
    [MEDIOSTRANS]         BIT           NULL,
    [PRODUCQUIM]          BIT           NULL,
    [JUGUETES]            BIT           NULL,
    [EQUIPOSCONS]         BIT           NULL,
    [VESTIMENTA]          BIT           NULL,
    [MATERIALESC]         BIT           NULL,
    [MUEBLES]             BIT           NULL,
    [ARTICULOSNIN]        BIT           NULL,
    [ARTICULOSDEP]        BIT           NULL,
    [EQUIPOSCOM]          BIT           NULL,
    [ARTICULOSBELLE]      BIT           NULL,
    [MEDICAMENTOS]        BIT           NULL,
    [APARATOLOGIA]        BIT           NULL,
    [EQUIPOSBIOMED]       BIT           NULL,
    [HOGAR]               BIT           NULL,
    [ESTABLEDUCA]         BIT           NULL,
    [CALLE]               BIT           NULL,
    [LUGARECREA]          BIT           NULL,
    [INDUSTRIA]           BIT           NULL,
    [ESTABLEPUBLI]        BIT           NULL,
    [CENTROESTET]         BIT           NULL,
    [SPA]                 BIT           NULL,
    [IPS]                 BIT           NULL,
    [NUMPROCE]            INT           NULL,
    [TIPOPROFES]          INT           NULL,
    [HOSPITAL]            BIT           NULL,
    [UCI]                 BIT           NULL,
    [VERSION]             VARCHAR (20)  NULL,
    [JSON]                VARCHAR (MAX) NULL,
    CONSTRAINT [PK_HCFICHA453] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [CK_HCFICHA453_JSON] CHECK (isjson([JSON])=(1)),
    CONSTRAINT [FK_HCFICHA453_HCFICHANOTIFICACION] FOREIGN KEY ([IDFICHANOTIFICACION]) REFERENCES [dbo].[HCFICHANOTIFICACION] ([ID])
);


GO
ALTER TABLE [dbo].[HCFICHA453] NOCHECK CONSTRAINT [CK_HCFICHA453_JSON];




GO
ALTER TABLE [dbo].[HCFICHA453] NOCHECK CONSTRAINT [CK_HCFICHA453_JSON];


GO



GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Nuevas columnas en formato JSON    Desde versión "V01_2020-03-06" -->  - SEAN_SSSN = bit   - NOMBRE_ELEMENTO_LESION = varchar(100)  - SUSTANCIA_NICOTINA =  bit   - SUSTANCIA_SABORIZANTE =bit     - SUSTANCIA_MARIHUANA = bit   - SUSTANCIA_OTRA = bit   - SUSTANCIA_CUAL = varchar(100)  - FRECUENCIA = bit   - MC_TOS = bit   - MC_DISNEA = bit    - MC_DIFICULTAD_RESPIRATORIA = bit   - MC_DOLOR_TORACICO = bit   - MC_NAUSEAS = bit   - MC_VOMITO = bit   - MC_DIARREA = bit   - MC_DOLOR_ABDOMINAL = bit   - MC_OTRA = bit    - MC_CUAL = varchar(100)  - DX_SBO = bit   - DX_ECA = bit   - DX_INTOXICACION = bit   - DX_QUEMADURA = bit   - DX_ALERGIA = bit   - OTRA_CIGARRILLO = bit   - OTRA_MARIHUANA = bit   - OTRA_COCAINA = bit   - OTRA_BAZUCO = bit   - OTRA_HEROINA = bit   - ANTECEDENTE_ASMA = bit   - ANTECEDENTE_EPOC = bit   - ANTECEDENTE_ALERGIA_RESPIRATORIA = bit   - ANTECEDENTE_FIBROSIS_QUISTICA = bit   - ANTECEDENTE_ENFERMEDAD_CORONARIA = bit     ; donde MC: Manifestaciones Clinicas , DX : Diagnostico,   SEAN_SSSN: Sistemas Electrónicos de Administración de Nicotina (SEAN) / Sistemas Electrónicos sin suministro de Nicotina (SSSN) ,   DX_SBO: Dx Síndrome Bronquial Obstructivo, DX_ECA: Dx Evento coronario agudo.   ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA453', @level2type = N'COLUMN', @level2name = N'JSON';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Versión de la ficha de notificación ---> Si es valor nulo = primer versión ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA453', @level2type = N'COLUMN', @level2name = N'VERSION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda UCI 1  =  Si   0 = No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA453', @level2type = N'COLUMN', @level2name = N'UCI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda hospitalizacion  1=Si     0 =  No ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA453', @level2type = N'COLUMN', @level2name = N'HOSPITAL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda el  Tipo de profesional que desarrollo el procedimiento estético:  1 =profesional de la salud   2 =Cirujano plástico  3 =Médico esteticista   4 =Médico especialista  5 =Esteticista  6 = Cosmetólogo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA453', @level2type = N'COLUMN', @level2name = N'TIPOPROFES';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda el Número de procedimientos quirurgicos que se realizaron de manera simultanea  1 = 1      2 = 2        3 = 3    4 = mas de 3', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA453', @level2type = N'COLUMN', @level2name = N'NUMPROCE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda el lugar de ocurrencia del evento IPS   1 =  Si   0 =  No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA453', @level2type = N'COLUMN', @level2name = N'IPS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda el lugar de ocurrencia del evento SPA   1 =  Si   0 =  No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA453', @level2type = N'COLUMN', @level2name = N'SPA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda el lugar de ocurrencia del evento Lugar recreacionalCentro de estética   1 =  Si   0 =  No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA453', @level2type = N'COLUMN', @level2name = N'CENTROESTET';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda el lugar de ocurrencia del evento Lugar recreacional Establecimiento público   1 =  Si   0 =  No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA453', @level2type = N'COLUMN', @level2name = N'ESTABLEPUBLI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda el lugar de ocurrencia del evento Lugar recreacional  Industria   1 =  Si   0 =  No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA453', @level2type = N'COLUMN', @level2name = N'INDUSTRIA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda el lugar de ocurrencia del evento Lugar recreacional   1 =  Si   0 =  No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA453', @level2type = N'COLUMN', @level2name = N'LUGARECREA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda el lugar de ocurrencia del evento  Calle   1 =  Si   0 =  No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA453', @level2type = N'COLUMN', @level2name = N'CALLE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda el lugar de ocurrencia del evento  Establecimiento educativo   1 =  Si   0 =  No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA453', @level2type = N'COLUMN', @level2name = N'ESTABLEDUCA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda el lugar de ocurrencia del evento  Hogar   1 =  Si   0 =  No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA453', @level2type = N'COLUMN', @level2name = N'HOGAR';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda elemento que causo la lesión Equipos biomédicos   1 =  Si   0 =  No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA453', @level2type = N'COLUMN', @level2name = N'EQUIPOSBIOMED';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda elemento que causo la lesión Aparatología de uso estético    1 =  Si   0 =  No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA453', @level2type = N'COLUMN', @level2name = N'APARATOLOGIA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda elemento que causo la lesión Medicamentos   1 =  Si   0 =  No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA453', @level2type = N'COLUMN', @level2name = N'MEDICAMENTOS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda elemento que causo la lesión Articulos de belleza, cuidado personal e higiene     1 =  Si   0 =  No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA453', @level2type = N'COLUMN', @level2name = N'ARTICULOSBELLE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda elemento que causo la lesión Equipos de comunicación, electrónicos, equipos audiovisuales y computadores    1 =  Si   0 =  No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA453', @level2type = N'COLUMN', @level2name = N'EQUIPOSCOM';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda elemento que causo la lesión  Articulos deportivos  1 =  Si   0 =  No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA453', @level2type = N'COLUMN', @level2name = N'ARTICULOSDEP';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda elemento que causo la lesión  Articulos o elementos de niños  1 =  Si   0 =  No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA453', @level2type = N'COLUMN', @level2name = N'ARTICULOSNIN';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda elemento que causo la lesión Muebles,  elcetricos  1 =  Si   0 =  No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA453', @level2type = N'COLUMN', @level2name = N'MUEBLES';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda elemento que causo la lesión  Material escolar  1 =  Si   0 =  No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA453', @level2type = N'COLUMN', @level2name = N'MATERIALESC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda elemento que causo la lesión Vestimenta, accesorios y calzado   1  = Si  0 = No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA453', @level2type = N'COLUMN', @level2name = N'VESTIMENTA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda elemento que causo la lesión  Equipos de costrucción  1 =  Si   0 =  No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA453', @level2type = N'COLUMN', @level2name = N'EQUIPOSCONS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda elemento que causo lalesión  Juguetes  1 =  Si   0 =  No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA453', @level2type = N'COLUMN', @level2name = N'JUGUETES';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda elemento que causo lalesión  Productos quimicos  1 =  Si   0 =  No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA453', @level2type = N'COLUMN', @level2name = N'PRODUCQUIM';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda elemento que causo lalesión  Medio de trasporte   1 =  Si   0 =  No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA453', @level2type = N'COLUMN', @level2name = N'MEDIOSTRANS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda elemento que causo lalesión   Máquina, equipo eléctrico o motor  1 =  Si   0 =  No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA453', @level2type = N'COLUMN', @level2name = N'MAQUINA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '( campo eliminado desde version V01_2020-03-06 )   Glúteos:   0 - No  1 - Si  ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA453', @level2type = N'COLUMN', @level2name = N'GLUTEOS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda parte del cuerpo afectada  es seleccionada  Antebrazo    true = si    false = no', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA453', @level2type = N'COLUMN', @level2name = N'ANTEBRAZO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda parte del cuerpo afectada  es seleccionada Genitales    true = si    false = no', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA453', @level2type = N'COLUMN', @level2name = N'GENITALES';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda parte del cuerpo afectada  es seleccionada  Brazo    true = si    false = no', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA453', @level2type = N'COLUMN', @level2name = N'BRAZO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda parte del cuerpo afectada  es seleccionada Pelvis perineo    true = si    false = no', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA453', @level2type = N'COLUMN', @level2name = N'PELVISPERI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda parte del cuerpo afectada  es seleccionada  Cuello    true = si    false = no', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA453', @level2type = N'COLUMN', @level2name = N'CUELLO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda parte del cuerpo afectada  es seleccionada Piel    true = si    false = no', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA453', @level2type = N'COLUMN', @level2name = N'PIEL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda parte del cuerpo afectada  es seleccionada   Abdomen   true = si    false = no', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA453', @level2type = N'COLUMN', @level2name = N'ABDOMEN';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda parte del cuerpo afectada  es seleccionada    Boca dientes   true = si    false = no', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA453', @level2type = N'COLUMN', @level2name = N'BOCADIENTES';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda parte del cuerpo afectada  es seleccionada     Organos internos   true = si    false = no', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA453', @level2type = N'COLUMN', @level2name = N'ORGANINTER';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda parte del cuerpo afectada  es seleccionada     MAMAS   true = si    false = no', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA453', @level2type = N'COLUMN', @level2name = N'MAMAS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda parte del cuerpo afectada  es seleccionada     OREJAS   true = si    false = no', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA453', @level2type = N'COLUMN', @level2name = N'OREJAS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda parte del cuerpo afectada  es seleccionada     DEDOS PIES   true = si    false = no', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA453', @level2type = N'COLUMN', @level2name = N'DEDOSPIES';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda parte del cuerpo afectada  es seleccionada     Tórax posterior   true = si    false = no', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA453', @level2type = N'COLUMN', @level2name = N'TORAXPOST';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda parte del cuerpo afectada  es seleccionada     NARIZ   true = si    false = no', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA453', @level2type = N'COLUMN', @level2name = N'NARIZ';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda parte del cuerpo afectada  es seleccionada     PIES   true = si    false = no', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA453', @level2type = N'COLUMN', @level2name = N'PIES';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda parte del cuerpo afectada  es seleccionada     Tórax anterior   true = si    false = no', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA453', @level2type = N'COLUMN', @level2name = N'TORAXANTE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda parte del cuerpo afectada  es seleccionada  OJOS    true = si    false = no', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA453', @level2type = N'COLUMN', @level2name = N'OJOS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda parte del cuerpo afectada  es seleccionada  PIERNAS   true = si    false = no', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA453', @level2type = N'COLUMN', @level2name = N'PIERNAS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda parte del cuerpo afectada  es seleccionada  Dedos mano   true = si    false = no', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA453', @level2type = N'COLUMN', @level2name = N'DEDOSMANO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda parte del cuerpo afectada  es seleccionada  CARA   true = si    false = no', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA453', @level2type = N'COLUMN', @level2name = N'CARA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda parte del cuerpo afectada  es seleccionada  MUSLOS   true = si    false = no', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA453', @level2type = N'COLUMN', @level2name = N'MUSLOS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda parte del cuerpo afectada  es seleccionada  MANO   true = si    false = no', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA453', @level2type = N'COLUMN', @level2name = N'MANO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda parte del cuerpo afectada  es seleccionada  CRANEO   true = si    false = no', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA453', @level2type = N'COLUMN', @level2name = N'CRANEO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda si el tipo de lesión  es seleccionada  DEPRESIÓN RESPIRATORIA   true = si    false = no', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA453', @level2type = N'COLUMN', @level2name = N'DEPRESION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda si el tipo de lesión  es seleccionada EMBOLIA   true = si    false = no', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA453', @level2type = N'COLUMN', @level2name = N'EMBOLIA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda si el tipo de lesión  es seleccionada NECROSIS   true = si    false = no', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA453', @level2type = N'COLUMN', @level2name = N'NECROSIS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda si el tipo de lesión  es seleccionada HEMORRAGIA   true = si    false = no', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA453', @level2type = N'COLUMN', @level2name = N'HEMORRAGIA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda si el tipo de lesión  es seleccionada PERFORACIÓN   true = si    false = no', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA453', @level2type = N'COLUMN', @level2name = N'PERFORACION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda si el tipo de lesión  es seleccionada SEPSIS   true = si    false = no', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA453', @level2type = N'COLUMN', @level2name = N'SEPSIS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda si el tipo de lesión  es seleccionada INFECCIÓN   true = si    false = no', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA453', @level2type = N'COLUMN', @level2name = N'INFECCION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda si el tipo de lesión  es seleccionada INTOXICACIÓN   true = si    false = no', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA453', @level2type = N'COLUMN', @level2name = N'INTOXICACION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda si el tipo de lesión  es seleccionada QUEMADURAS   true = si    false = no', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA453', @level2type = N'COLUMN', @level2name = N'QUEMADURAS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda si el tipo de lesión  es seleccionada AMPUTACIÓN   true = si    false = no', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA453', @level2type = N'COLUMN', @level2name = N'AMPUTACION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda si el tipo de lesión  es seleccionada Politraumatismo   true = si    false = no', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA453', @level2type = N'COLUMN', @level2name = N'POLITRAUMA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda si el tipo de lesión  es seleccionada  FRACTURA   true = si    false = no', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA453', @level2type = N'COLUMN', @level2name = N'FRACTURA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda si el tipo de lesión  es seleccionada   Choque eléctrico, electrocución   true = si    false = no', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA453', @level2type = N'COLUMN', @level2name = N'CHOQUEELEC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda si el tipo de lesión  es seleccionada TRAUMA LEVE   true = si    false = no', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA453', @level2type = N'COLUMN', @level2name = N'TRAUMALEV';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda si el tipo de lesión  es seleccionada HERIDA   true = si    false = no', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA453', @level2type = N'COLUMN', @level2name = N'HERIDA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda si el tipo de lesión  es seleccionada ESTRANGULA   true = si    false = no', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA453', @level2type = N'COLUMN', @level2name = N'ESTRANGULA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda si el tipo de lesión  es seleccionada  ASFIXIA   true = si    false = no', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA453', @level2type = N'COLUMN', @level2name = N'ASFIXIA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda la   Lesión de causa externa ocasionada por:  1 = Accidente de Consumo  0 = Procedimientos Estéticos', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA453', @level2type = N'COLUMN', @level2name = N'LESIOCAUSAEXT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Guarda  codigo diagnostico', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA453', @level2type = N'COLUMN', @level2name = N'CODDIAGNO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda el ID de ficha Notificacion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA453', @level2type = N'COLUMN', @level2name = N'IDFICHANOTIFICACION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Guarda el consecutivo de la tabla', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA453', @level2type = N'COLUMN', @level2name = N'ID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Registro de lesiones y eventos adversos notificados en la ficha 453 de vigilancia epidemiológica (SIVIGILA). Guarda el diagnóstico, el tipo de lesión, las partes del cuerpo afectadas, los agentes causantes, el lugar del evento y el profesional que reporta, asociados a una notificación de caso.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA453';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA453';
