#Region "Imports"

Imports Infrastructure.Data.Base
Imports Domain.Entities


#End Region

Public Class OperatingUnitRepository
    Inherits GenericRepository(Of OperatingUnit)
    Implements IOperatingUnitRepository

    Private _context As IGlobalModelUnitOfWork

    Public Sub New(ByVal context As IGlobalModelUnitOfWork)
        MyBase.New(context)
        _context = context
    End Sub

    ''' <summary>
    ''' obtiene una unidad operativa por codigo
    ''' </summary>
    ''' <param name="code">codigo de la unidad operativa</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetOpertatingUnitByCode(code As String) As OperatingUnit Implements IOperatingUnitRepository.GetOpertatingUnitByCode
        If code Is Nothing OrElse code.Trim().Equals(String.Empty) Then
            Throw New ArgumentNullException("code")
        End If
        Dim res = (From d As OperatingUnit In Me._context.OperatingUnit
                   Where d.UnitCode.Equals(code.Trim())
                   Select d).FirstOrDefault

        If res IsNot Nothing Then

            If res.IdUnit IsNot Nothing Then
                Dim operatingUnit = (From st In _context.OperatingUnit.AsNoTracking Where st.Id = res.IdUnit Select st).FirstOrDefault
                res.OperatingUnitDescription = operatingUnit.UnitCode + " - " + operatingUnit.UnitName
            End If

            If res.IdCity IsNot Nothing Then
                Dim city = (From c In _context.City.AsNoTracking Where c.Id = res.IdCity Select c).FirstOrDefault
                res.CityDescription = city.Code + " - " + city.Name
            End If

            res.OriginalValue = (From g In _context.OperatingUnit.AsNoTracking
                                  Where g.UnitCode.Equals(code.Trim())
                                  Select g).FirstOrDefault

            Return res
        Else
            Return New OperatingUnit()
        End If
    End Function

    ''' <summary>
    ''' obtiene una unidad operativa por id
    ''' </summary>
    ''' <param name="id">id de la unidad operativa</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetOperatingUnitById(id As Integer) As OperatingUnit Implements IOperatingUnitRepository.GetOperatingUnitById
        If id = 0 Then
            Throw New ArgumentNullException("id")
        End If
        Dim res = (From d In Me._context.OperatingUnit Where d.Id = id Select d).ToList
        If res.Count > 0 Then
            res.SingleOrDefault.OriginalValue = (From d As OperatingUnit In Me._context.OperatingUnit.AsNoTracking() Where d.Id = id Select d).SingleOrDefault()
            Return res.SingleOrDefault
        Else
            Return New OperatingUnit()
        End If
    End Function

    ''' <summary>
    ''' Obtiene todas las unidades operativas
    ''' </summary>
    ''' <returns>Lista de unidades operativas</returns>
    ''' <remarks></remarks>
    Public Function ListAllOperatingUnit() As List(Of OperatingUnit) Implements IOperatingUnitRepository.ListAllOperatingUnit
        Dim query = From e In _context.OperatingUnit.Include("City").Include("City.Department").Include("City.Department.Country")
                    Select e
        Return query.ToList()
    End Function

End Class
