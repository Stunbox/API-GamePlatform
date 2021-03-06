const express = require('express');
const mysql = require('mysql');

const bodyParser = require('body-parser');

const PORT = process.env.PORT || 3050;

const app = express();

app.use(bodyParser.json());

//My sql
const connection = mysql.createConnection({
    host: 'gameplatform.cwhbwaghxtul.us-east-2.rds.amazonaws.com',
    user: 'admin',
    password: '12345678',
    database: 'nodejsDB'
});

//Route

//Users
app.get('/users', (req, res) => {
    const sql = 'SELECT * FROM users';
    const response = {
        Items:""
    }
    connection.query(sql, (error, results) => {
        if (error) throw error;
        if (results.length > 0) {
            response.Items = results;
            res.send(response);
        } else {
            res.send('Not results');
        }
    });

});
app.get('/users/:id', (req, res) => {
    const { id } = req.params;

    const sql = `SELECT * FROM users WHERE id = ${id}`;
    connection.query(sql, (error, result) => {
        if (error) throw error;
        if (result.length > 0) {
            res.json(result);
        } else {
            res.send('Not results');
        }
    });

});
app.post('/users/add', (req, res) => {
    const sql = 'INSERT INTO users SET ?';
    const userObj = {
        name: req.body.name,
        coins: req.body.coins
    }
    
    const response = {
        status_message: "success",
        success: true
    }
    connection.query(sql, userObj, error => {
        if (error) {
            response.status_message = error;
            response.success = false;
            //throw error;
        } else {
            res.send(response);
        }

    });
});
app.put('/users/:id', (req, res) => {
    const { id } = req.params;
    const { name, coins } = req.body;
    const sql = `UPDATE users SET name = '${name}', coins='${coins}' WHERE id=${id}`;

    const response = {
        status_message: "success",
        success: true
    }
    connection.query(sql, error => {
        if (error) throw error;
        res.send(response);
    });
});
app.delete('/users/:id', (req, res) => {
    const { id } = req.params;
    const sql = `DELETE FROM users WHERE id=${id}`;

    const response = {
        status_message: "success",
        success: true
    }

    connection.query(sql, error => {
        if (error) {
            response.status_message = error;
            response.success = false;
            //throw error;
        }
        else {
            res.send(response);
        }
    });
});
//Stats ------------
app.get('/users/:id/stats', (req, res) => {
    const { id } = req.params;
    const sql = `SELECT * FROM stats WHERE id_user = ${id}`;
    const response = {
        Items:""
    }
    connection.query(sql, (error, results) => {
        if (error) throw error;
        if (results.length > 0) {
            response.Items = results;
            res.send(response);
        } else {
            res.send('Not results');
        }
    });

});

//Check conect
connection.connect(error => {
    if (error) throw error;
    console.log('Database server runing!');
});

app.listen(PORT, () => console.log(`server running on port ${PORT}`));