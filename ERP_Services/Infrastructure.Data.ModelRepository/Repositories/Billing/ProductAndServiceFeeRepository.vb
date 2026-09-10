
#Region "Imports"

Imports Infrastructure.Data.Base
Imports Domain.Entities
Imports System.Data.Entity.Infrastructure
Imports Infrastructure.Data.SecurityRepository


#End Region

Public Class ProductAndServiceFeeRepository
    Inherits GenericRepository(Of ProductAndServiceFee)
    Implements IProductAndServiceFeeRepository

    ''' <summary>
    ''' The _context
    ''' </summary>
    Private _context As IGlobalModelUnitOfWork

    ''' <summary>
    ''' Esta variable  contiene el contexto de nuestro modelo.
    ''' </summary>
    Private _contextSecuriry As ISeguridadUnitOfWork

    ''' <summary>
    ''' Inicia el contexto de facturacion
    ''' </summary>
    ''' <param name="context">The context.</param>
    Public Sub New(ByVal context As IGlobalModelUnitOfWork, ByVal contextSecurity As ISeguridadUnitOfWork)
        MyBase.New(context)
        _context = context
        _contextSecuriry = contextSecurity
    End Sub

#Region "Methods"

    ''' <summary>
    ''' Obtiene una tarifa de productos y servicios por Id
    ''' </summary>
    Public Function GetProductAndServiceFeeById(Id As Integer) As ProductAndServiceFee Implements IProductAndServiceFeeRepository.GetProductAndServiceFeeById

        Dim query = (From p In _context.ProductAndServiceFee.Include("ProductFeeDetail").Include("ServiceFeeDetail").Include("ProductAndServiceFeeUser")
                     Where p.Id.Equals(Id)
                     Select p).FirstOrDefault()


        If query IsNot Nothing Then
            Return query
        Else
            Return New ProductAndServiceFee()
        End If
    End Function

    ''' <summary>
    ''' Obtiene una tarifa de productos y servicios por codigo
    ''' </summary>
    Public Function GetProductProductAndServiceFeeByCode(code As String) As ProductAndServiceFee Implements IProductAndServiceFeeRepository.GetProductAndServiceFeeByCode

        Dim query = (From p In _context.ProductAndServiceFee.Include("ProductFeeDetail").Include("ServiceFeeDetail").Include("ProductAndServiceFeeUser")
                     Where p.Code.Equals(code)
                     Select p).FirstOrDefault()

        If query IsNot Nothing Then

            If query.ProductFeeDetail IsNot Nothing And query.ProductFeeDetail.Any() Then
                For Each item In query.ProductFeeDetail
                    item.ProductCodeName = (From ip In _context.InventoryProduct.AsNoTracking Where ip.Id = item.ProductId Select String.Concat(ip.Code, " - ", ip.Name)).FirstOrDefault
                    item.RateTypeName = IIf(item.RateType, "Porcentaje", "Tarifa fija")

                    If item.RateType Then
                        item.PercentageTypeName = IIf(Not item.PercentageType, "Costo Promedio Ponderado", "Ultimo Costo")
                    End If
                Next
            End If

            If query.ServiceFeeDetail IsNot Nothing And query.ServiceFeeDetail.Any() Then
                For Each item In query.ServiceFeeDetail
                    item.ServiceCodeName = (From bc In _context.BillingConcept.AsNoTracking Where bc.Id = item.ServiceId Select String.Concat(bc.Code, " - ", bc.Name)).FirstOrDefault
                Next
            End If

            If query.ProductAndServiceFeeUser IsNot Nothing And query.ProductAndServiceFeeUser.Any() Then
                For Each item In query.ProductAndServiceFeeUser
                    item.FullNameUser = (From e In _contextSecuriry.User.Include("Person") Where e.Id = item.UserId Select e.Person.Fullname).FirstOrDefault
                Next
            End If

            Return query
        Else
            Return New ProductAndServiceFee()
        End If
    End Function


    ''' <summary>
    '''  Guarda una tarifa de productos y servicios
    ''' </summary>
    ''' <param name="EntityXml"></param>
    ''' <param name="codeUser"></param>
    ''' <returns></returns>
    Public Function SP_SaveProductAndServiceFee(EntityXml As String, codeUser As String) As SP_SaveProductAndServiceFee_Result Implements IProductAndServiceFeeRepository.SP_SaveProductAndServiceFee
        DirectCast(_context, IObjectContextAdapter).ObjectContext.CommandTimeout = 3600
        Return _context.SP_SaveProductAndServiceFee(EntityXml, codeUser).SingleOrDefault
    End Function

    ''' <summary>
    ''' Copiar y pegar para el formulario
    ''' </summary>
    ''' <param name="XmlObject"></param>
    ''' <returns></returns>
    Function SetProductFeeDetailFromFile(xmlObject As String) As List(Of SP_CopyAndPasteProductFeeDetails_Result) Implements IProductAndServiceFeeRepository.SetProductFeeDetailFromFile
        DirectCast(_context, IObjectContextAdapter).ObjectContext.CommandTimeout = 3600
        Return _context.SP_CopyAndPasteProductFeeDetails(xmlObject).ToList()
    End Function

    ''' <summary>
    ''' Copiar y pegar para el formulario
    ''' </summary>
    ''' <param name="XmlObject"></param>
    ''' <returns></returns>
    Function SetServiceProductFeeDetailFromFile(xmlObject As String) As List(Of SP_CopyAndPasteServiceFeeDetails_Result) Implements IProductAndServiceFeeRepository.SetServiceProductFeeDetailFromFile
        DirectCast(_context, IObjectContextAdapter).ObjectContext.CommandTimeout = 3600
        Return _context.SP_CopyAndPasteServiceFeeDetails(xmlObject).ToList()
    End Function

#End Region

End Class