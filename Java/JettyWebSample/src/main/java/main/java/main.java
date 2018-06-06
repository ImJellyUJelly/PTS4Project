package main.java;

import javax.naming.CommunicationException;
import javax.security.auth.login.LoginException;
import javax.servlet.Servlet;
import javax.servlet.ServletException;
import javax.servlet.http.HttpServlet;
import javax.servlet.http.HttpServletRequest;
import javax.servlet.http.HttpServletResponse;
import javax.servlet.http.HttpSession;
import java.io.IOException;
import java.io.PrintWriter;

public class main extends HttpServlet {

    //private main.java.Seizure seizure;
    private String message;

    @Override
    public void init() throws ServletException {
        message = "Dit is een test... わ";
    }

    @Override
    protected void doGet(HttpServletRequest req, HttpServletResponse resp) throws ServletException, IOException {


        String method = req.getParameter("method");
        String user = req.getParameter("user");
        String pass = req.getParameter("pass");

//        if (user != null & pass != null){
//            req.logout();
//            req.login(user, pass);
//
//        }
        resp.setContentType("text/xml;charset=UTF-8");
        PrintWriter writer = resp.getWriter();
//        writer.append("<?xml version=\"1.0\" encoding=\"UTF-8\"?>");
//        writer.append("<midi_song>");
        if(method != null){
            if(method.equals("login")){
                login(writer, user, pass);
            }
            else if(method == "getSong"){
                getSong(writer);
            }
        }
        else {
            writer.append("Unauthenticated.");
        }

//        if (user != null){
//            writer.append("Method: " + method + ", User: " + user + ", Password: "+pass+ "  " );
//        }


//        writer.append("SESSION ID:" + req.getSession().getId());
//
//
//        writer.append("</midi_song>");


    }
    private PrintWriter login(PrintWriter writer, String user, String pass){
        //check login data in main.java.database
        if(user.equals("test") && pass.equals("test")){
            writer.append("User: " + user + ", Password: "+ pass + ".");
        }
        else{
            writer.append("false");
        }
        return writer;
}
    private PrintWriter getSong(PrintWriter writer){
        //get song from main.java.database
        //seizure.loadMidiString(0); // sum id?
        return writer;
    }

    @Override
    public void destroy() {
        //
    }
}
