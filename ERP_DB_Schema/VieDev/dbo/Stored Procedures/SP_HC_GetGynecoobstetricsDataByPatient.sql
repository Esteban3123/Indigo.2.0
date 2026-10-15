CREATE proc [dbo].[SP_HC_GetGynecoobstetricsDataByPatient]
  @ingres char(10)
, @sheetnumber char(10)
, @patient varchar(25)
as
begin
	declare @recorddata as table (numingres varchar(20), numefolio varchar(20), ipsexopac int, fechispac datetime, imc decimal, nomsemges numeric, fecpropar datetime)
	insert into @recorddata
		select
			a.numingres
			,a.numefolio
			,d.ipsexopac
			,c.fechispac
			,(cast(b.pesopacie as decimal)/1000)/((cast(b.tallapaci as decimal)/ 100)*(cast(b.tallapaci as decimal)/ 100)) imc
			,a.nomsemges
			,a.fecpropar
		from
			hcantgine as a
			left join hcexfisic b on b.ipcodpaci = a.ipcodpaci and b.numingres = a.numingres and b.numefolio = a.numefolio
			left join hchispaca c on b.ipcodpaci = a.ipcodpaci and c.numingres = a.numingres and c.numefolio = a.numefolio
			left join inpacient d on d.ipcodpaci = a.ipcodpaci 
		where
			a.ipcodpaci = @patient and c.fechispac <= (select fechispac from hchispaca where numingres = @ingres and numefolio = @sheetnumber)
		order by c.fechispac

	declare @estimatedduedate datetime = (select top 1 fecpropar from @recorddata order by fechispac desc)

	select 
		 imc
		,nomsemges
	from
		@recorddata
	where
		fechispac > @estimatedduedate
end