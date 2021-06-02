using ExamenFinal.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ExamenFinal.Rpts;
using System.IO;
using CrystalDecisions.Shared;
using CrystalDecisions.CrystalReports.Engine;

namespace ExamenFinal.Controllers
{
    public class HomeController : Controller
    {
        neptunoEntities contexto = new neptunoEntities();
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(usuarios us)
        {
            var usuario = contexto.usuarios.Where(c => c.username.Equals(us.username) && c.pass.Equals(us.pass)).FirstOrDefault();
            if (usuario != null)
            {
                Session["usuario"] = usuario;
         
                        return Json(new { result = "/Home/Reportes" });       
            }

            return Json(new { result = false });
        }

        public ActionResult Reportes()
        {
            ViewBag.Message = "Reportes";

            return View();
        }
        public ActionResult CerrarSession()
        {
            Session.Clear();
            return RedirectToAction("Index");
        }

        public ActionResult ReporteEmpleados()
        {
            usuarios us = (usuarios)Session["usuario"];
            if(us == null)
            {
                return RedirectToAction("Index");
            }
            List<string> ciudades = new List<string>();

            var listaEmpleados = contexto.Empleados.ToList();
            foreach(Empleados em in listaEmpleados)
            {
                if (!ciudades.Contains(em.ciudad))
                {
                    ciudades.Add(em.ciudad);
                }
            }
            ViewBag.ciudades = ciudades;
            return View();
        }

        public ActionResult VerReporteEmpleados(string ciudad)
        {
            var reporte = new ReporteEmpleados();
            reporte.FileName = Server.MapPath("/Rpts/ReporteEmpleados.rpt");
            reporte.SetParameterValue("ciudad", ciudad);
            var coninfo = getConexion();
            TableLogOnInfo logoninfo = new TableLogOnInfo();
            Tables tables;
            tables = reporte.Database.Tables;
            reporte.DataSourceConnections.Clear();
            foreach (Table item in tables)
            {
                logoninfo = item.LogOnInfo;
                logoninfo.ConnectionInfo = coninfo;
                item.ApplyLogOnInfo(logoninfo);
            }
            Response.Buffer = false;
            Response.ClearContent();
            Response.ClearHeaders();
            Stream stream = reporte.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
            stream.Seek(0, SeekOrigin.Begin);
            return new FileStreamResult(stream, "application/pdf");
        }

        public ConnectionInfo getConexion()
        {
            ConnectionInfo crconnectioninfo = new ConnectionInfo();
            crconnectioninfo.ServerName = ".";
            crconnectioninfo.DatabaseName = "neptuno";
            crconnectioninfo.IntegratedSecurity = true;
            return crconnectioninfo;
        }


        public ActionResult ReporteProductoCantidadProducto()
        {
            usuarios us = (usuarios)Session["usuario"];
            if (us == null)
            {
                return RedirectToAction("Index");
            }
            List<string> tipo = new List<string>();
            tipo.Add("caja");
            tipo.Add("latas");
            tipo.Add("paq");
            tipo.Add("bolsas");
            tipo.Add("frascos");
            ViewBag.tipos = tipo;
            return View();
        }

        public ActionResult VerReporteProductoCantidadProducto(string tipo)
        {
            var reporte = new ReporteProductosPorUnidad();
            reporte.FileName = Server.MapPath("/Rpts/ReporteProductosPorUnidad.rpt");
            reporte.SetParameterValue("tipo", tipo);
            var coninfo = getConexion();
            TableLogOnInfo logoninfo = new TableLogOnInfo();
            Tables tables;
            tables = reporte.Database.Tables;
            reporte.DataSourceConnections.Clear();
            foreach (Table item in tables)
            {
                logoninfo = item.LogOnInfo;
                logoninfo.ConnectionInfo = coninfo;
                item.ApplyLogOnInfo(logoninfo);
            }
            Response.Buffer = false;
            Response.ClearContent();
            Response.ClearHeaders();
            Stream stream = reporte.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
            stream.Seek(0, SeekOrigin.Begin);
            return new FileStreamResult(stream, "application/pdf");
        }

        public ActionResult ReportePedidosPorEmpleado()
        {
            usuarios us = (usuarios)Session["usuario"];
            if (us == null)
            {
                return RedirectToAction("Index");
            }
            else
            {
                if (us.nivel == 2)
                {
                    return RedirectToAction("Reportes");
                }
            }
            var empledos = contexto.Empleados.ToList();
            ViewBag.empleados = empledos;
            return View();
        }

        public ActionResult VerReportePedidosPorEmpleado(string nombre, string apellidos)
        {
            var reporte = new ReportePedidosEmpleados();
            reporte.FileName = Server.MapPath("/Rpts/ReportePedidosEmpleados.rpt");
            reporte.SetParameterValue("nombre", nombre);
            reporte.SetParameterValue("apellidos", apellidos);
            var coninfo = getConexion();
            TableLogOnInfo logoninfo = new TableLogOnInfo();
            Tables tables;
            tables = reporte.Database.Tables;
            reporte.DataSourceConnections.Clear();
            foreach (Table item in tables)
            {
                logoninfo = item.LogOnInfo;
                logoninfo.ConnectionInfo = coninfo;
                item.ApplyLogOnInfo(logoninfo);
            }
            Response.Buffer = false;
            Response.ClearContent();
            Response.ClearHeaders();
            Stream stream = reporte.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
            stream.Seek(0, SeekOrigin.Begin);
            return new FileStreamResult(stream, "application/pdf");
        }

        public ActionResult ReporteProductosProveedor()
        {
            usuarios us = (usuarios)Session["usuario"];
            if (us == null)
            {
                return RedirectToAction("Index");
            }
            var proveedores = contexto.proveedores.ToList();
            ViewBag.proveedores = proveedores;
            return View();
        }

        public ActionResult VerReporteProductosProveedor(int idProveedor)
        {
            var reporte = new ProductosPorProveedor();
            reporte.FileName = Server.MapPath("/Rpts/ProductosPorProveedor.rpt");
            reporte.SetParameterValue("idProveedor", idProveedor);
            var coninfo = getConexion();
            TableLogOnInfo logoninfo = new TableLogOnInfo();
            Tables tables;
            tables = reporte.Database.Tables;
            reporte.DataSourceConnections.Clear();
            foreach (Table item in tables)
            {
                logoninfo = item.LogOnInfo;
                logoninfo.ConnectionInfo = coninfo;
                item.ApplyLogOnInfo(logoninfo);
            }
            Response.Buffer = false;
            Response.ClearContent();
            Response.ClearHeaders();
            Stream stream = reporte.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
            stream.Seek(0, SeekOrigin.Begin);
            return new FileStreamResult(stream, "application/pdf");
        }

        public ActionResult ReporteDetallePedidosPorPedido()
        {
            usuarios us = (usuarios)Session["usuario"];
            if (us == null)
            {
                return RedirectToAction("Index");
            }
            else
            {
                if(us.nivel == 2)
                {
                    return RedirectToAction("Reportes");
                }
            }
            var pedidos = contexto.Pedidos.ToList();
            ViewBag.pedidos = pedidos;
            return View();
        }

        public ActionResult VerReporteDetallePedidosPorPedido(int idPedido)
        {
            var reporte = new ReporteDetallesPedido();
            reporte.FileName = Server.MapPath("/Rpts/ReporteDetallesPedido.rpt");
            reporte.SetParameterValue("idPedido", idPedido);
            var coninfo = getConexion();
            TableLogOnInfo logoninfo = new TableLogOnInfo();
            Tables tables;
            tables = reporte.Database.Tables;
            reporte.DataSourceConnections.Clear();
            foreach (Table item in tables)
            {
                logoninfo = item.LogOnInfo;
                logoninfo.ConnectionInfo = coninfo;
                item.ApplyLogOnInfo(logoninfo);
            }
            Response.Buffer = false;
            Response.ClearContent();
            Response.ClearHeaders();
            Stream stream = reporte.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
            stream.Seek(0, SeekOrigin.Begin);
            return new FileStreamResult(stream, "application/pdf");
        }

    }
}