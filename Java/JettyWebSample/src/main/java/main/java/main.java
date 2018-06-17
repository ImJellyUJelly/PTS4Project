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
    private Seizure seizure = new Seizure();

    @Override
    public void init() throws ServletException {
        message = "Dit is een test... わ";
    }

    @Override
    protected void doGet(HttpServletRequest req, HttpServletResponse resp) throws ServletException, IOException {
        String method = req.getParameter("method");
        String user = req.getParameter("user");
        String pass = req.getParameter("pass");
        String payload = req.getParameter("payload");
        resp.setContentType("text/xml;charset=UTF-8");
        PrintWriter writer = resp.getWriter();
//        writer.append("webservice reached");
//        login(writer, "user", "pass" );

        if(method != null){
            if(method.equals("login")){
                if(seizure.login(user,pass)){
                    writer.append("Welcome "+user+".");
                }
                else{
                    writer.append("User does not exsist.");
                }
            }
            else if(method.equals("getSong")){
                seizure.loadMidiString(1);
            }
            else if(method.equals("testSongSave")){
                 seizure.saveMidiByte(payload);
                 writer.append(seizure.readMidiByte());
//                System.out.println(payload);
            }
            else if(method.equals("saveSong")){
                seizure.saveMidiString("");
            }
        }
        else {
            writer.append("Internal error404: No method found.");
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
