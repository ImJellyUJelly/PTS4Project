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

    private String message;

    @Override
    public void init() throws ServletException {
        message = "Dit is een test... わ";
    }

    @Override
    protected void doGet(HttpServletRequest req, HttpServletResponse resp) throws ServletException, IOException {



        String user = req.getParameter("user");
        String pass = req.getParameter("pass");

        if (user != null & pass != null){
            req.logout();
            req.login(user, pass);

        }

        resp.setContentType("text/xml;charset=UTF-8");
        PrintWriter writer = resp.getWriter();
        writer.append("<?xml version=\"1.0\" encoding=\"UTF-8\"?>");
        writer.append("<midi_song>");

        if (user != null){
            writer.append("Logged in as: " + user);
        } else {
            writer.append("Unauthenticated.");
        }

        writer.append("SESSION ID:" + req.getSession().getId());


        writer.append("</midi_song>");


    }

    @Override
    public void destroy() {
        //
    }
}
