
CREATE PROCEDURE [dbo].[SPINV_CrearTablaAuditoriaSuministro]
(
@TablaCabecera Char(15),
@TablaDetalle Char(15)
)
AS
BEGIN
	SET NOCOUNT ON;
	DECLARE @sql AS NVARCHAR(MAX)
	SET @sql = 'CREATE TABLE [dbo].[' + @TablaCabecera + '] (
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[CODCONCEC] [int] NOT NULL,
	[FECGENLOG] [datetime] NOT NULL,
	[USUARIO] [varchar](20) NOT NULL,
	[MENSAJE] [varchar](2000) NULL,
	[PROESTADO] [bit] NULL,
	[CODCENATE] [char](10) NOT NULL,
	[UFUCODIGO] [char](10) NOT NULL,
	[TIPORDEN] [tinyint] NOT NULL,
 CONSTRAINT [PK_' + @TablaCabecera + '] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
ALTER TABLE [dbo].[' + @TablaCabecera + ']  WITH CHECK ADD  CONSTRAINT [FK_' + @TablaCabecera + '_ADCENATEN] FOREIGN KEY([CODCENATE])
REFERENCES [dbo].[ADCENATEN] ([CODCENATE])

ALTER TABLE [dbo].[' + @TablaCabecera + '] CHECK CONSTRAINT [FK_' + @TablaCabecera + '_ADCENATEN]

ALTER TABLE [dbo].[' + @TablaCabecera + ']  WITH CHECK ADD  CONSTRAINT [FK_' + @TablaCabecera + '_INUNIFUNC] FOREIGN KEY([UFUCODIGO])
REFERENCES [dbo].[INUNIFUNC] ([UFUCODIGO])

ALTER TABLE [dbo].[' + @TablaCabecera + '] CHECK CONSTRAINT [FK_' + @TablaCabecera + '_INUNIFUNC]

CREATE TABLE [dbo].[' + @TablaDetalle + '](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[LOGSUMID] [int] NOT NULL,
	[PROCODCUM] [varchar](50) NULL,
	[PROCODIGO] [char](20)  NULL,
	[PRONOMBRE] [varchar](500)  NULL,
	[CODLOTE] [varchar](50) NULL,
	[INNLOTSER] [varchar](100) NULL,
	[CANTIDAD] [int] NOT NULL,
	[INNMSUMPA] [varchar](100) NULL,
	[NUMSUMINIS] [varchar](50) NULL,
	[INNDOCUME] [varchar](100) NULL,
 CONSTRAINT [PK_' + @TablaDetalle + '] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

ALTER TABLE [dbo].[' + @TablaDetalle + ']  WITH CHECK ADD  CONSTRAINT [FK_' + @TablaDetalle + '_' + @TablaCabecera + '] FOREIGN KEY([LOGSUMID])
REFERENCES [dbo].[' + @TablaCabecera + '] ([ID])

ALTER TABLE [dbo].[' + @TablaDetalle + '] CHECK CONSTRAINT [FK_' + @TablaDetalle + '_' + @TablaCabecera + ']

'
EXEC sp_executesql @sql
	
end