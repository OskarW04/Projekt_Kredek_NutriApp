import React from "react";
import { useEffect, useState } from "react";
import { Link, useNavigate } from "react-router-dom";
import "../App.css";

import Table from "@mui/material/Table";
import TableBody from "@mui/material/TableBody";
import TableCell from "@mui/material/TableCell";
import TableContainer from "@mui/material/TableContainer";
import TableHead from "@mui/material/TableHead";
import TableRow from "@mui/material/TableRow";
import Paper from "@mui/material/Paper";
import Button from "@mui/material/Button";
import Stack from "@mui/material/Stack";

import axios from "axios";

const apiUrl = import.meta.env.VITE_API_URL;

async function sendRequest(token) {
  try {
    const getDetails = await axios.get(`${apiUrl}/api/UserDetails`, {
      headers: {
        Authorization: `Bearer ${token}`,
        Accept: "application/json",
        "Content-Type": "application/json",
      },
      params: { email: localStorage.getItem("email") },
    });

    return getDetails.data;
  } catch (error) {
    window.alert(error);
    return [];
  }
}

function createData(name, value) {
  return { name, value };
}

export const UserDetailsGet = () => {
  const navigate = useNavigate();
  const [details, setDetails] = useState([]);
  const [rows, setRows] = useState([]);

  useEffect(() => {
    sendRequest(localStorage.getItem("token")).then((x) => setDetails(x));
    const rowsInit = [
      createData("Email", details.email),
      createData("Name", details.name),
      createData("Last Name", details.lastName),
      createData("Age", details.age),
      createData("Height", details.height),
      createData("weight", details.weight),
      createData("Goal", details.nutritionGoal),
    ];
    setRows(rowsInit);
  }, [
    details.age,
    details.email,
    details.height,
    details.lastName,
    details.name,
    details.nutritionGoal,
    details.weight,
  ]);

  return (
    <>
      <TableContainer component={Paper}>
        <Table sx={{ minWidth: 650 }} aria-label="simple table">
          <TableHead>
            <TableRow>
              <TableCell>User Details</TableCell>
              <TableCell align="right">Values</TableCell>
            </TableRow>
          </TableHead>
          <TableBody>
            {rows.map((row) => (
              <TableRow
                key={row.name}
                sx={{ "&:last-child td, &:last-child th": { border: 0 } }}
              >
                <TableCell component="th" scope="row">
                  {row.name}
                </TableCell>
                <TableCell align="right">{row.value}</TableCell>
              </TableRow>
            ))}
          </TableBody>
        </Table>
      </TableContainer>
      <br />
      <Stack spacing={2} direction="row" style={{ alignItems: "center" }}>
        <Button
          variant="contained"
          onClick={() => {
            navigate("/Home");
          }}
        >
          Back
        </Button>
        <Button
          variant="contained"
          onClick={() => {
            navigate("/Details/Update", { state: details });
          }}
        >
          Update Details
        </Button>
      </Stack>
    </>
  );
};
