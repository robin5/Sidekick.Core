function addBlankRow() {

    // The row Id of the next blank question row is
    // the same as the number of class "question-row" rows found
    const rowId = document.querySelectorAll(".question-row").length;

    // Get the survey-questions div and add a blank row to it
    document.querySelector("#survey-questions").append(BlankQuestion(rowId));
}

function BlankQuestion(rowId) {

    /* create a div that resembles the following
    <div id="row-id-0 + class="question-row survey-question-container">
        <div class="survey-question-box">'
            <textarea id="question-0 class="q-textarea form-control" name="Questions[0]" required ></textarea>
        </div>
        <div class="survey-delete-box">
            <a href="#" data-row-id=0 onclick="deleteRow(event)">delete</a>
        </div>
    </div>
    */
    const row = document.createElement("div");
    row.id = `row-id-${rowId}`;
    row.classList.add("question-row");
    row.classList.add("survey-question-container");

    const questionDiv = document.createElement("div");
    questionDiv.classList.add("survey-question-box");
    row.appendChild(questionDiv);

    const textarea = document.createElement("textarea");
    textarea.id = `question-${rowId}`;
    textarea.name = `Questions[${rowId}]`;
    textarea.classList.add("q-textarea");
    textarea.classList.add("form-control");
    textarea.required = true;
    questionDiv.appendChild(textarea);

    const deleteDiv = document.createElement("div");
    deleteDiv.classList.add("survey-delete-box");
    row.appendChild(deleteDiv);

    const deleteAnchor = document.createElement("a");
    deleteAnchor.href = "#0";
    deleteAnchor.setAttribute("data-row-id", rowId);
    deleteAnchor.classList.add("delete-anchor");
    deleteAnchor.appendChild(document.createTextNode("delete"));

    // On the click event delete a row from the table and prevent the anchor from firing.
    deleteAnchor.onclick = function(event) {
        const rowId = "#row-id-" + event.target.getAttribute("data-row-id");
        if (null != rowId) {
            const rowToDelete = document.querySelector(rowId);
            if (null != rowToDelete) {
                rowToDelete.remove();
                enumerateQuestions();
            }
        }
        event.preventDefault();
    };
    deleteDiv.appendChild(deleteAnchor);

    return row;
}

// Deletes a row from the table and prevents the anchor from firing.
function deleteRow(event) {
    const rowId = "#row-id-" + event.target.getAttribute("data-row-id");
    if (null != rowId) {
        const rowToDelete = document.querySelector(rowId);
        if (null != rowToDelete) {
            rowToDelete.remove();
            enumerateQuestions();
        }
    }
    event.preventDefault();
}

// Enumerates the question input elements so they will be transmitted
// as an array that is zero indexed and not sparse
function enumerateQuestions() {

    // Get the div containing the survey question
    const rows = document.querySelectorAll(".question-row");

    // for each row in the div
    for (var rowId = 0; rowId < rows.length; ++rowId) {

        var row = rows[rowId];

        // rename the rows id attribute
        row.id = `row-id-${rowId}`;

        // rename the textarea's id and name attributes
        let textarea = row.querySelector(".q-textarea");
        if (null != textarea) {
            textarea.id = `question-${rowId}`;
            textarea.name = `Questions[${rowId}]`;
        }

        let anchor = row.querySelector(".delete-anchor");
        if (null != anchor) {
            anchor.setAttribute("data-row-id", rowId);
        }
    }
}
